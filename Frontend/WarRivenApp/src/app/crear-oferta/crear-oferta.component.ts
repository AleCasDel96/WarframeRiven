import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OfertaService } from '../services/oferta.service';
import { RivenService, Riven } from '../services/riven.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-crear-oferta',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './crear-oferta.component.html'
})
export class CrearOfertaComponent implements OnInit {
  rivens: Riven[] = [];
  idRivenSeleccionado = '';
  precioVenta = 0;
  error = '';

  constructor(
    private rivenService: RivenService,
    private ofertaService: OfertaService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.rivenService.getMisRivens().subscribe({
      next: (data) => this.rivens = data,
      error: () => this.error = 'No se pudieron cargar tus Rivens.'
    });
  }

  crearOferta() {
    if (!this.idRivenSeleccionado || this.precioVenta <= 0) {
      this.error = 'Selecciona un Riven y un precio válido.';
      return;
    }

    this.ofertaService.crear({
      idRiven: this.idRivenSeleccionado,
      precioVenta: this.precioVenta
    }).subscribe({
      next: () => this.router.navigate(['/ofertas']),
      error: (e) => {
        this.error = e.error || 'Error al crear la oferta.';
      }
    });
  }
}
