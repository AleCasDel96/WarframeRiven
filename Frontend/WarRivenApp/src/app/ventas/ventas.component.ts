import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VentaService, Venta } from '../services/venta.service';
import { RivenService, Riven } from '../services/riven.service';

@Component({
  selector: 'app-ventas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ventas.component.html',
  styleUrls: ['./ventas.component.css']
})
export class VentasComponent implements OnInit {
  ventas: Venta[] = [];
  error = '';
  rivenSeleccionado: Riven | null = null;
  popupX = 0;
  popupY = 0;
  showPopup = false;

  constructor(
    private ventaService: VentaService,
    private rivenService: RivenService
  ) {}

  ngOnInit(): void {
    this.ventaService.getMisVentas().subscribe({
      next: data => this.ventas = data,
      error: () => this.error = 'No se pudieron cargar las ventas.'
    });
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
