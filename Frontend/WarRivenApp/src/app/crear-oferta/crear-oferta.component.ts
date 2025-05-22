import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';

import { OfertaService } from '../services/oferta.service';
import { Oferta } from '../models/oferta.model';

@Component({
  selector: 'app-crear-oferta',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './crear-oferta.component.html'
})
export class CrearOfertaComponent {
   idRiven?: string;

  precio: number = 0;
  error = '';

  constructor(private ofertaService: OfertaService, private router: Router, private route: ActivatedRoute) {}

ngOnInit(): void {
  this.idRiven =this.idRiven=this.route.snapshot.paramMap.get('idRiven') || ''
}

  crearOferta() {
    this.idRiven=this.route.snapshot.paramMap.get('idRiven') || ''
    if (!this.idRiven) {
      this.error = 'No se ha seleccionado un Riven válido.';
      return;
    }

    const nuevaOferta: Partial<Oferta> = {
      idRiven: this.idRiven,
      precioVenta: this.precio
    };
    this.ofertaService.crear(nuevaOferta).subscribe({
      next: () => this.router.navigate(['/mis-ofertas']),
      error: () => this.error = 'No se pudo crear la oferta.'
    });
  }
}