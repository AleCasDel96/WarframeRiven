import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Riven } from '../models/riven.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RivenService {
  private apiUrl = 'https://localhost:5001/api/Rivens';

  constructor(private http: HttpClient) {}

  // Obtener los rivens del usuario logueado
  getMisRivens(): Observable<Riven[]> {
    return this.http.get<Riven[]>(this.apiUrl + '/');
  }

  // Obtener un riven por su ID
  getPorId(id: string): Observable<Riven> {
    return this.http.get<Riven>(`${this.apiUrl}/${id}`);
  }

  // Crear un riven
  crear(riven: Partial<Riven>): Observable<any> {
    return this.http.post(this.apiUrl, riven);
  }

  // Editar un riven
  editar(id: string, riven: Partial<Riven>): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, riven);
  }

  // Eliminar un riven
  eliminar(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
