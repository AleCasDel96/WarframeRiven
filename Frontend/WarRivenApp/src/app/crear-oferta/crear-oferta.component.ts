import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';

import { OfertaService } from '../services/oferta.service';
import { Oferta } from '../models/oferta.model';

@Component({
  selector: 'app-crear-oferta',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './crear-oferta.component.html'
})
export class CrearOfertaComponent {
  @Input() idRivenSeleccionado?: string;

  precio: number = 0;
  error = '';

  constructor(private ofertaService: OfertaService, private router: Router) {}

  crearOferta(event: Event) {
    event.preventDefault();
    if (!this.idRivenSeleccionado) {
      this.error = 'No se ha seleccionado un Riven válido.';
      return;
    }

    const nuevaOferta: Partial<Oferta> = {
      idRiven: this.idRivenSeleccionado,
      precioVenta: this.precio,
      disponibilidad: false,
      partida: false,
      destino: false
    };

    this.ofertaService.crear(nuevaOferta).subscribe({
      next: () => this.router.navigate(['/mis-ofertas']),
      error: () => this.error = 'No se pudo crear la oferta.'
    });
  }
}

