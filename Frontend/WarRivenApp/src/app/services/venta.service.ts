import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Venta } from '../models/venta.model';
import { Observable } from 'rxjs';
import { log } from 'node:console';
import { VentaDTO } from '../models/ventaDTO.model';

@Injectable({
  providedIn: 'root'
})
export class VentaService {
  private apiUrl = 'https://localhost:5001/api/Ventas';

  constructor(private http: HttpClient) { }

  // Obtener historial de ventas del usuario logueado

  getMisVentas(): Observable<VentaDTO[]>{
    return this.http.get<VentaDTO[]>(`${this.apiUrl}/MisVentas/`);
  }

  obtenerVenta(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Venta/${id}`);
  }

  confirmarCompra(id: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/${id}`, {});
  }
  confirmarVenta(id: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/Confirmar/${id}`, {});
  }

  eliminarVenta(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/eliminar/${id}`);
  }
}
