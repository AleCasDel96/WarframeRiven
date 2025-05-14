import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfertaService, Oferta } from '../services/oferta.service';

@Component({
  selector: 'app-mis-pujas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mis-pujas.component.html'
})
export class MisPujasComponent implements OnInit {
  pujas: Oferta[] = [];
  error = '';

  constructor(private ofertaService: OfertaService) { }

  confirmarCompra(id: string) {
    this.ofertaService.confirmarCompra(id).subscribe({
      next: () => this.ngOnInit(),
      error: () => this.error = 'No se pudo confirmar la compra.'
    });
  }

  ngOnInit(): void {
    this.ofertaService.getMisPujas().subscribe({
      next: data => this.pujas = data,
      error: () => this.error = 'No se pudieron cargar tus pujas.'
    });
  }
}
