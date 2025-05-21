import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Venta } from '../models/venta.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VentaService {
  private apiUrl = 'https://localhost:5001/api/Ventas';

  constructor(private http: HttpClient) {}

  // Obtener historial de ventas del usuario logueado
  getHistorial(): Observable<Venta[]> {
    return this.http.get<Venta[]>(`${this.apiUrl}/Historial`);
  }

  getMisVentas(): Observable<any[]> {
  return this.http.get<any[]>('/api/Ventas/MisVentas');
}

  obtenerVenta(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Venta/${id}`);
  }

  confirmarVenta(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Confirmar/${id}`);
  }

  confirmarCompra(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Vendido/${id}`);
  }

  eliminarVenta(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/eliminar/${id}`);
  }
}
