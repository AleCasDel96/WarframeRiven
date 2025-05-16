import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';

import { RivenService } from '../services/riven.service';
import { Riven } from '../models/riven.model';

@Component({
  selector: 'app-crear-riven',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './crear-riven.component.html'
})
export class CrearRivenComponent {
  riven: Partial<Riven> = {
    nombre: '',
    arma: '',
    stat1: '',
    valor1: 0
  };

  error = '';

  constructor(private rivenService: RivenService, private router: Router) {}

  crear() {
    if (!this.riven.nombre || !this.riven.arma || !this.riven.stat1 || this.riven.valor1 === undefined) {
      this.error = 'Debes completar al menos la estadística principal.';
      return;
    }

    this.rivenService.crear(this.riven).subscribe({
      next: () => this.router.navigate(['/mis-rivens']),
      error: () => this.error = 'No se pudo crear el Riven.'
    });
  }
}
