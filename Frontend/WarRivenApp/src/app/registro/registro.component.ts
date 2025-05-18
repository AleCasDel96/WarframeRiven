import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-registro',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './registro.component.html'
})
export class RegistroComponent {
  nickNuevo = '';
  emailNuevo = '';
  passNuevo = '';
  confirmPass = '';
  mensajeRegistro = '';

  constructor(private auth: AuthService, private router: Router) { }

  registrar() {
    this.mensajeRegistro = '';

    if (this.passNuevo !== this.confirmPass) {
      this.mensajeRegistro = 'Las contraseñas no coinciden.';
      return;
    }

    this.auth.register(this.emailNuevo, this.passNuevo, this.nickNuevo).subscribe({
      next: () => {
        this.mensajeRegistro = 'Usuario registrado correctamente. Ahora puedes iniciar sesión.';
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 1500);
      },
      error: err => {
        this.mensajeRegistro = err.error || 'Error al registrar usuario.';
      }
    });
  }
}