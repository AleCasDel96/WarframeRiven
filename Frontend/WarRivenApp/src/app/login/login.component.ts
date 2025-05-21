import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  // Login
  email = '';
  password = '';
  mensajeError = '';

  mostrarRegistro = false;

  constructor(private auth: AuthService, private router: Router) { }

  ngOnInit(): void {
    if (this.auth.estaLogueado()) {
      // Solo cerrar sesión si vienes explícitamente de un logout
      const logoutForzado = sessionStorage.getItem('forceLogout');
      if (logoutForzado) {
        this.auth.logout();
        sessionStorage.removeItem('forceLogout');
      }
    }
  }

  goToRegistro() {
    this.router.navigate(['/registro']);
  }

  login(event: Event) {
    event.preventDefault();
    this.auth.login(this.email, this.password).subscribe({
      next: (token) => {
        this.auth.saveToken(token);
        this.router.navigate(['/']);
      },
      error: () => {
        this.mensajeError = 'Credenciales incorrectas.';
      }
    });
  }

}
