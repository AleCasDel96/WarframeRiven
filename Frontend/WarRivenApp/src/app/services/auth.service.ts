import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:5001/api/Auth';

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<string> {
    return this.http.post(this.apiUrl + '/Login', { email, password }, { responseType: 'text' });
  }

  register(email: string, password: string, nickname: string): Observable<any> {
    return this.http.post(this.apiUrl + '/Register', {
    email: email,
    password: password,
    nickname: nickname
  });
  }

  saveToken(token: string): void {
    if (typeof window !== 'undefined') {
      localStorage.setItem('jwt', token);
    }
  }

  update(tipo: string, valor: string) {
  if (tipo === 'Upgrade') {
    return this.http.get('api/UserConf/Upgrade');
  }
  return this.http.put(`api/UserConf/${tipo}`, valor, {
    headers: { 'Content-Type': 'application/json' }
  });
}

  getToken(): string | null {
    if (typeof window === 'undefined') return null;
    return localStorage.getItem('jwt');
  }

  getNickname(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const payload = token.split('.')[1];
      const decoded = JSON.parse(atob(payload));
      return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] || null;
    } catch (e) {
      console.warn('Error decoding JWT:', e);
      return null;
    }
  }

  logout() {
    if (typeof window !== 'undefined') {
      localStorage.removeItem('jwt');
    }
  }

  estaLogueado(): boolean {
    return !!this.getToken();
  }
}
