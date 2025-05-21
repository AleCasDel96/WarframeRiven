import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';

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

  constructor(private auth: AuthService) { }

  cambiar(tipo: string, valor: string) {
    this.auth.update(tipo, valor).subscribe({
      next: () => {
        // Si el cambio fue de warframe nick, también realiza el upgrade
        if (tipo === 'ChangeWarNick') {
          this.auth.update('Upgrade', '').subscribe({
            next: () => this.mensaje = 'Warframe Nick y rol actualizados.',
            error: err => this.mensaje = 'Warframe Nick cambiado, pero fallo al actualizar el rol.'
          });
        } else {
          this.mensaje = 'Cambio realizado con éxito.';
        }
      },
      error: () => {
        this.mensaje = 'Error al realizar el cambio.';
      }
    });
  }

  irASteam() {
    //
  }
}
