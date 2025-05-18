import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { timer, Observable, merge, of } from 'rxjs';
import { map, switchMap, catchError, timeout } from 'rxjs/operators';
import { Alerta } from '../models/alerta.model';

interface CicloAPI { state: string; timeLeft: string; }
interface NightwaveAPI { active: boolean; activeChallenges?: { eta: string }[]; }
interface SortieAPI { boss: string; eta: string; variants: { missionType: string; node: string; modifier: string }[]; }
interface Fissure { missionType: string; tier: string; eta: string; }

@Injectable({ providedIn: 'root' })
export class WarframeAlertService {
  constructor(private http: HttpClient) {}

  private parseTimeLeft(timeLeft: string | undefined): Date | null {
    if (!timeLeft) return null;
    const partes = timeLeft.split(' ');
    if (partes.length !== 2) return null;
    const [minStr, secStr] = partes;
    const minutos = parseInt(minStr);
    const segundos = parseInt(secStr);
    if (isNaN(minutos) || isNaN(segundos)) return null;
    return new Date(Date.now() + minutos * 60000 + segundos * 1000);
  }

  getTodasAlertas$(): Observable<Alerta[]> {
    const cadaMinuto$ = timer(0, 60000);
    const timeoutMs = 5000;

    return merge(
      cadaMinuto$.pipe(switchMap(() =>
        this.http.get<CicloAPI>('https://api.warframestat.us/pc/cetusCycle').pipe(
          timeout(timeoutMs),
          map(data => {
            const exp = this.parseTimeLeft(data.timeLeft);
            return exp ? [{
              fuente: 'Cetus',
              mensaje: `Estado: ${data.state}`,
              fechaExpiracion: exp
            }] : [];
          }),
          catchError(() => of([{ fuente: 'Cetus', mensaje: 'No disponible' }]))
        )
      )),
      cadaMinuto$.pipe(switchMap(() =>
        this.http.get<CicloAPI>('https://api.warframestat.us/pc/vallisCycle').pipe(
          timeout(timeoutMs),
          map(data => {
            const exp = this.parseTimeLeft(data.timeLeft);
            return exp ? [{
              fuente: 'Fortuna',
              mensaje: `Estado: ${data.state}`,
              fechaExpiracion: exp
            }] : [];
          }),
          catchError(() => of([{ fuente: 'Fortuna', mensaje: 'No disponible' }]))
        )
      )),
      cadaMinuto$.pipe(switchMap(() =>
        this.http.get<CicloAPI>('https://api.warframestat.us/pc/cambionCycle').pipe(
          timeout(timeoutMs),
          map(data => {
            const exp = this.parseTimeLeft(data.timeLeft);
            return exp ? [{
              fuente: 'Deriva Cambion',
              mensaje: `Estado: ${data.state}`,
              fechaExpiracion: exp
            }] : [];
          }),
          catchError(() => of([{ fuente: 'Deriva Cambion', mensaje: 'No disponible' }]))
        )
      )),
      cadaMinuto$.pipe(switchMap(() =>
        this.http.get<NightwaveAPI>('https://api.warframestat.us/pc/nightwave').pipe(
          timeout(timeoutMs),
          map(data => {
            const challenge = data.activeChallenges?.[0];
            const exp = challenge ? this.parseTimeLeft(challenge.eta) : null;
            return exp ? [{
              fuente: 'Nightwave',
              mensaje: `Desafío activo`,
              fechaExpiracion: exp
            }] : [];
          }),
          catchError(() => of([{ fuente: 'Nightwave', mensaje: 'No disponible' }]))
        )
      )),
      cadaMinuto$.pipe(switchMap(() =>
        this.http.get<SortieAPI>('https://api.warframestat.us/pc/sortie').pipe(
          timeout(timeoutMs),
          map(data => {
            const exp = this.parseTimeLeft(data.eta);
            const alertas: Alerta[] = exp ? [{
              fuente: 'Sortie',
              mensaje: `Jefe: ${data.boss}`,
              fechaExpiracion: exp
            }] : [];
            for (const v of data.variants) {
              alertas.push({
                fuente: 'Sortie',
                mensaje: `${v.missionType} en ${v.node} - ${v.modifier}`
              });
            }
            return alertas;
          }),
          catchError(() => of([{ fuente: 'Sortie', mensaje: 'No disponible' }]))
        )
      )),
      cadaMinuto$.pipe(switchMap(() =>
        this.http.get<Fissure[]>('https://api.warframestat.us/pc/fissures').pipe(
          timeout(timeoutMs),
          map(data =>
            data.map(f => {
              const exp = this.parseTimeLeft(f.eta);
              return {
                fuente: 'Fissure',
                mensaje: `${f.missionType} (${f.tier})`,
                fechaExpiracion: exp || undefined
              };
            })
          ),
          catchError(() => of([{ fuente: 'Fissure', mensaje: 'No disponible' }]))
        )
      ))
    );
  }
}
