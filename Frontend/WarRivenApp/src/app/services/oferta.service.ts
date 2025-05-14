import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Oferta {
  id: string;
  idRiven: string;
  idComprador: string;
  precioVenta: number;
  disponibilidad: boolean;
  partida: boolean;
  destino: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class OfertaService {
  private apiUrl = 'https://localhost:5001/api/Ofertas';

  constructor(private http: HttpClient) {}

  // Ofertas disponibles públicamente
  getPublicas(): Observable<Oferta[]> {
    return this.http.get<Oferta[]>(this.apiUrl);
  }

  // Ofertas propias del usuario autenticado
  getMisOfertas(): Observable<Oferta[]> {
    return this.http.get<Oferta[]>(`${this.apiUrl}/mias`);
  }

  getMisPujas(): Observable<Oferta[]> {
  return this.http.get<Oferta[]>(`${this.apiUrl}/mispujas`);
}

  // Crear nueva oferta
  crear(oferta: Partial<Oferta>): Observable<Oferta> {
    return this.http.post<Oferta>(this.apiUrl, oferta);
  }

  // Abrir o cerrar una oferta
  abrir(id: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/open/${id}`, {});
  }

  cerrar(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/open/${id}`);
  }

  // Confirmar oferta (vendedor / comprador)
  confirmarVenta(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/ComfirVen/${id}`);
  }

  confirmarCompra(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/ComfirCom/${id}`);
  }
 
  // Realizar el traspaso (crear venta y transferir)
  transpaso(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Transpaso/${id}`);
  }

  // Eliminar una oferta
  eliminar(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
