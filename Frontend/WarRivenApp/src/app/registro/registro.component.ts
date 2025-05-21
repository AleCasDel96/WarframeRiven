import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './registro.component.html',
  styleUrls: ['./registro.component.css']
})
export class RegistroComponent {
  nickNuevo = '';
  emailNuevo = '';
  passNuevo = '';
  confirmPass = '';
  mensajeRegistro = '';

  constructor(private auth: AuthService) {}

  registrar() {
  console.log('🔥 Angular interceptó el submit');
    if (this.passNuevo !== this.confirmPass) {
      this.mensajeRegistro = 'Las contraseñas no coinciden';
      return;
    }

    this.auth.register(this.emailNuevo, this.passNuevo, this.nickNuevo).subscribe({
      next: () => {
        this.mensajeRegistro = 'Registro exitoso';
      },
      error: () => {
        this.mensajeRegistro = 'Error en el registro';
      }
    });
  }
}