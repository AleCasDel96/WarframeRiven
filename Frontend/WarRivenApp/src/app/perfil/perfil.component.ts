import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-perfil',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './perfil.component.html'
})
export class PerfilComponent {
  nickname = '';
  warframeNick = '';
  icono = '';
  nuevaPassword = '';
  mensaje = '';

  private apiUrl = 'https://localhost:5001/api/UserConf';

  constructor(private http: HttpClient, private auth: AuthService) {}

  cambiar(field: 'ChangeNick' | 'ChangeWarNick' | 'ChangeIcon' | 'ChangePass', value: string) {
    this.http.put(`${this.apiUrl}/${field}`, value, { responseType: 'text' }).subscribe({
      next: () => this.mensaje = `${field} actualizado correctamente.`,
      error: () => this.mensaje = `Error al actualizar ${field}.`
    });
  }

  irASteam() {
    //
  }
}
