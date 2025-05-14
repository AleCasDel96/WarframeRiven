import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfertaService, Oferta } from '../services/oferta.service';
import { RivenService, Riven } from '../services/riven.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mis-ofertas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mis-ofertas.component.html',
  styleUrls: ['./mis-ofertas.component.css']
})
export class MisOfertasComponent implements OnInit {
  ofertas: Oferta[] = [];
  error = '';

  rivenSeleccionado: Riven | null = null;
  popupX = 0;
  popupY = 0;
  showPopup = false;

  constructor(
    private ofertaService: OfertaService,
    private rivenService: RivenService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.ofertaService.getMisOfertas().subscribe({
      next: data => this.ofertas = data,
      error: () => this.error = 'No se pudieron cargar tus ofertas.'
    });
  }

  cerrar(id: string) {
    this.ofertaService.cerrar(id).subscribe(() => this.ngOnInit());
  }

  abrir(id: string) {
    this.ofertaService.abrir(id).subscribe(() => this.ngOnInit());
  }

  confirmarVenta(id: string) {
    this.ofertaService.confirmarVenta(id).subscribe(() => this.ngOnInit());
  }

  confirmarCompra(id: string) {
    this.ofertaService.confirmarCompra(id).subscribe(() => this.ngOnInit());
  }

  transpaso(id: string) {
  this.ofertaService.transpaso(id).subscribe({
    next: () => this.ngOnInit(),
    error: () => this.error = 'No se pudo completar el traspaso.'
  });
}

  eliminar(id: string) {
    if (confirm('¿Estás seguro de eliminar esta oferta?')) {
      this.ofertaService.eliminar(id).subscribe(() => this.ngOnInit());
    }
  }

  mostrarRiven(id: string, event: MouseEvent) {
    this.popupX = event.clientX;
    this.popupY = event.clientY;
    this.rivenService.getRiven(id).subscribe({
      next: r => {
        this.rivenSeleccionado = r;
        this.showPopup = true;
      },
      error: () => this.rivenSeleccionado = null
    });
  }

  ocultarRiven() {
    this.showPopup = false;
  }
}
