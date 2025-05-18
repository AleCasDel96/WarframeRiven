import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarframeAlertService } from '../services/warframe-alert.service';
import { Alerta } from '../models/alerta.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sidebar-alertas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sidebar-alertas.component.html',
  styleUrls: ['./sidebar-alertas.component.css']
})
export class SidebarAlertasComponent implements OnInit, OnDestroy {
  alertas: (Alerta & { expiraEn?: string })[] = [];
  oculto = false;

  private timerInterval!: ReturnType<typeof setInterval>;
  private alertaSubscription!: Subscription;

  constructor(private alertService: WarframeAlertService) {}

  ngOnInit(): void {
    this.alertaSubscription = this.alertService.getTodasAlertas$().subscribe(alertas => {
      this.alertas = alertas.filter(a => !a.fechaExpiracion || a.fechaExpiracion.getTime() > Date.now());
      this.actualizarCuentaAtras();
    });

    this.timerInterval = setInterval(() => this.actualizarCuentaAtras(), 1000);
  }

  ngOnDestroy(): void {
    this.alertaSubscription?.unsubscribe();
    clearInterval(this.timerInterval);
  }

  actualizarCuentaAtras(): void {
    const ahora = Date.now();

    this.alertas = this.alertas.filter(alerta => {
      const expiracion = alerta.fechaExpiracion?.getTime();
      if (!expiracion) return true;

      const msRestante = expiracion - ahora;
      if (msRestante <= 0) return false;

      const min = Math.floor(msRestante / 60000);
      const seg = Math.floor((msRestante % 60000) / 1000);
      alerta.expiraEn = `${min}m ${seg < 10 ? '0' : ''}${seg}s`;
      return true;
    });
  }
}
