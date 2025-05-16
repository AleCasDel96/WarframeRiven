import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Oferta } from '../models/oferta.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OfertaService {
  private apiUrl = 'https://localhost:5001/api/Ofertas';

  constructor(private http: HttpClient) {}

  getMisOfertas(): Observable<Oferta[]> {
    return this.http.get<Oferta[]>(`${this.apiUrl}/MisOfertas`);
  }

  getMisPujas(): Observable<Oferta[]> {
    return this.http.get<Oferta[]>(`${this.apiUrl}/MisPujas`);
  }

  getPublicas(): Observable<Oferta[]> {
    return this.http.get<Oferta[]>(`${this.apiUrl}/Publicas`);
  }

  crear(oferta: Partial<Oferta>): Observable<any> {
    return this.http.post(this.apiUrl, oferta);
  }

  cerrar(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Cerrar/${id}`);
  }

  abrir(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Open/${id}`); // ✅ NUEVO MÉTODO
  }

  confirmarVenta(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/ComfirVen/${id}`);
  }

  confirmarCompra(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/ComfirCom/${id}`);
  }

  transpaso(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Traspaso/${id}`);
  }

  eliminar(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
