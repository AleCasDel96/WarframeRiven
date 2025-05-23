import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Oferta } from '../models/oferta.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OfertaService {
  private apiUrl = 'https://localhost:5001/api/Ofertas';

  constructor(private http: HttpClient) { }

  getMisOfertas(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/MisOfertas`);
  }

  getPublicas(): Observable<Oferta[]> {
    return this.http.get<Oferta[]>(`${this.apiUrl}/VerOfertas`);
  }

  editar(id: string, precio: number ): Observable<any> {
    console.log("editar: "+ precio);
    return this.http.put(`${this.apiUrl}/Editar/${id}`, precio);
  }

  crear(oferta: Partial<Oferta>): Observable<any> {
    return this.http.post(this.apiUrl, oferta);
  }

  cambiarDisponibilidad(id: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/Disponibilidad/${id}`);
  }

  eliminar(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Eliminar/${id}`);
  }
}
