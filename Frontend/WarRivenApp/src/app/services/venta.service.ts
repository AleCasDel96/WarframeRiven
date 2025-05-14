import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Venta {
  id: string;
  idRiven: string;
  idComprador: string;
  idVendedor: string;
  precioVenta: number;
  fechaVenta: string;
}

@Injectable({
  providedIn: 'root'
})
export class VentaService {
  private apiUrl = 'https://localhost:5001/api/Ventas';

  constructor(private http: HttpClient) {}

  getMisVentas(): Observable<Venta[]> {
    return this.http.get<Venta[]>(this.apiUrl);
  }
}
