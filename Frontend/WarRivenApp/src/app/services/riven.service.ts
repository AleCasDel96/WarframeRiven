import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Riven {
  id: string;
  nombre: string;
  arma: string;
  stat1: string;
  valor1: number;
  stat2?: string;
  valor2?: number;
  stat3?: string;
  valor3?: number;
  stat4?: string;
  valor4?: number;
}

@Injectable({
    providedIn: 'root'
})
export class RivenService {
    private apiUrl = 'https://localhost:5001/api/Rivens';

    constructor(private http: HttpClient) { }

    crearRiven(riven: Partial<Riven>): Observable<Riven> {
        return this.http.post<Riven>(this.apiUrl, riven);
    }

    getRiven(id: string): Observable<Riven> {
        return this.http.get<Riven>(`${this.apiUrl}/${id}`);
    }

    actualizarRiven(id: string, riven: Partial<Riven>): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/${id}`, riven);
    }

    eliminarRiven(id: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }

    getMisRivens(): Observable<Riven[]> {
        return this.http.get<Riven[]>(this.apiUrl);
    }
}
