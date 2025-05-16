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
  getMisVentas(): Observable<Venta[]> {
    return this.http.get<Venta[]>(`${this.apiUrl}/MisVentas`);
  }
}
