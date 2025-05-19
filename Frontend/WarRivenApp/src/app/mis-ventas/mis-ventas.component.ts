import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VentaService } from '../services/venta.service';
import { RivenService } from '../services/riven.service';
import { Oferta } from '../models/oferta.model';
import { Riven } from '../models/riven.model';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgPipesModule } from 'ngx-pipes';
import { MensajeService } from '../services/mensaje.service';

@Component({
  selector: 'app-mis-ventas',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxPaginationModule, NgPipesModule],
  templateUrl: './mis-ventas.component.html'
})
export class MisVentasComponent implements OnInit {
  ofertas: Oferta[] = [];
  searchText = '';
  sortColumn = 'nombreRiven';
  sortAsc = true;
  p = 1;
  error = '';

  filtroArma: string = '';
  armasDisponibles: string[] = [];

  rivenSeleccionado: Riven | null = null;
  popupX = 0;
  popupY = 0;
  showPopup = false;

  constructor(
    private ventaService: VentaService,
    private rivenService: RivenService,
    private mensajeService: MensajeService
  ) { }

  ngOnInit(): void {
    this.ventaService.getMisVentasActivas().subscribe({
      next: data => {
        this.ofertas = data;

        const armasSet = new Set(data.map(o => o.arma).filter(Boolean));
        this.armasDisponibles = Array.from(armasSet) as string[];
      },
      error: () => this.error = 'No se pudieron cargar tus pujas.'
    });
  }

  ordenarPor(col: string): void {
    if (this.sortColumn === col) {
      this.sortAsc = !this.sortAsc;
    } else {
      this.sortColumn = col;
      this.sortAsc = true;
    }
  }

  confirmarCompra(id: string): void {
    this.ventaService.confirmarCompra(id).subscribe(() => this.ngOnInit());
  }

  copiarMensaje(oferta: Oferta): void {
    if (oferta.disponibilidad) return;

    if (!oferta.nickUsuario || !oferta.arma || !oferta.nombreRiven) {
      this.mensajeService.set('Faltan datos para generar el mensaje.');
      return;
    }

    const mensaje = `/w ${oferta.nickUsuario} he visto tu riven ${oferta.arma} ${oferta.nombreRiven} en WarframeRivens`;
    navigator.clipboard.writeText(mensaje).then(() => {
      this.mensajeService.set('Mensaje copiado al portapapeles.');
    }).catch(() => {
      this.mensajeService.set('No se pudo copiar el mensaje.');
    });
  }

  mostrarRiven(idRiven: string, e: MouseEvent): void {
    this.popupX = e.clientX;
    this.popupY = e.clientY;
    this.rivenService.getPorId(idRiven).subscribe({
      next: (r: Riven) => {
        this.rivenSeleccionado = r;
        this.showPopup = true;
      },
      error: () => this.rivenSeleccionado = null
    });
  }

  ocultarRiven(): void {
    this.showPopup = false;
  }
}
