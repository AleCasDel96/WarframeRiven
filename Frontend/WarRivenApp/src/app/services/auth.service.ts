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

  register(email: string, password: string, nickname: string) {
    return this.http.post(this.apiUrl + '/Register', { email, password, nickname });
  }

  saveToken(token: string) {
    localStorage.setItem('jwt', token);
  }

  getToken(): string | null {
    return localStorage.getItem('jwt');
  }

  getNickname(): string | null {
    const token = this.getToken();
    if (!token) return null;

    const payload = token.split('.')[1];
    const decoded = JSON.parse(atob(payload));
    return decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
  }

  logout() {
    localStorage.removeItem('jwt');
  }
}
