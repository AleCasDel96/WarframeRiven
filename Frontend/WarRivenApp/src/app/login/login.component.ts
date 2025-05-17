import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  // Login
  email = '';
  password = '';
  mensajeError = '';

  // Registro
  nickNuevo = '';
  emailNuevo = '';
  passNuevo = '';
  confirmPass = '';
  mensajeRegistro = '';

  mostrarRegistro = false;

  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
  if (this.auth.estaLogueado()) {
    this.auth.logout();
  }
}

  login() {
    this.auth.login(this.email, this.password).subscribe({
      next: (token) => {
        this.auth.saveToken(token);
        this.router.navigate(['/rivens']);
      },
      error: () => {
        this.mensajeError = 'Credenciales incorrectas.';
      }
    });
  }
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
          this.mostrarRegistro = false;
        }, 1500);
      },
      error: err => {
        this.mensajeRegistro = err.error || 'Error al registrar usuario.';
      }
    });
  }
}
