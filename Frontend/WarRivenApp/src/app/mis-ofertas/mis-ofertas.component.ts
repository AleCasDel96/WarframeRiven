import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfertaService } from '../services/oferta.service';
import { RivenService } from '../services/riven.service';
import { Oferta } from '../models/oferta.model';
import { Riven } from '../models/riven.model';
import { Router } from '@angular/router';
import { NgPipesModule } from 'ngx-pipes';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-mis-ofertas',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NgxPaginationModule,
    NgPipesModule
  ],
  templateUrl: './mis-ofertas.component.html',
  styleUrls: ['./mis-ofertas.component.css']
})
export class MisOfertasComponent implements OnInit {
  ofertas: Oferta[] = [];
  error = '';

  searchText = '';
  sortColumn = 'nombreRiven';
  sortAsc = true;
  p = 1;

  filtroArma: string = '';
  armasDisponibles: string[] = [];

  rivenSeleccionado: Riven | null = null;
  popupX = 0;
  popupY = 0;
  showPopup = false;

  pujaOferta: Oferta | null = null;
  nuevaPuja = 0;
  errorPuja = '';

  constructor(
    private ofertaService: OfertaService,
    private rivenService: RivenService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.ofertaService.getMisOfertas().subscribe({
      next: data => {
        this.ofertas = data;

        // Extrae armas únicas para el filtro
        const armasSet = new Set(data.map(o => o.arma).filter(Boolean));
        this.armasDisponibles = Array.from(armasSet) as string[];
      },
      error: () => this.error = 'No se pudieron cargar tus ofertas.'
    });
  }

  ordenarPor(campo: string): void {
    if (this.sortColumn === campo) {
      this.sortAsc = !this.sortAsc;
    } else {
      this.sortColumn = campo;
      this.sortAsc = true;
    }
  }

  cerrar(id: string): void {
    this.ofertaService.cerrar(id).subscribe(() => this.ngOnInit());
  }

  abrir(id: string): void {
    this.ofertaService.abrir(id).subscribe(() => this.ngOnInit());
  }

  confirmarVenta(id: string): void {
    this.ofertaService.confirmarVenta(id).subscribe(() => this.ngOnInit());
  }

  confirmarCompra(id: string): void {
    this.ofertaService.confirmarCompra(id).subscribe(() => this.ngOnInit());
  }

  transpaso(id: string): void {
    this.ofertaService.transpaso(id).subscribe({
      next: () => this.ngOnInit(),
      error: () => this.error = 'No se pudo completar el traspaso.'
    });
  }

  eliminar(id: string): void {
    if (confirm('¿Estás seguro de eliminar esta oferta?')) {
      this.ofertaService.eliminar(id).subscribe(() => this.ngOnInit());
    }
  }

  abrirPuja(oferta: Oferta): void {
    this.pujaOferta = oferta;
    this.nuevaPuja = oferta.precioVenta || 0;
    this.errorPuja = '';
  }

  cerrarPuja(): void {
    this.pujaOferta = null;
  }

  confirmarPuja(): void {
    if (!this.pujaOferta) return;

    const id = this.pujaOferta.id!;
    this.ofertaService.editar(id, { precioVenta: this.nuevaPuja }).subscribe({
      next: () => {
        this.pujaOferta = null;
        this.ngOnInit();
      },
      error: () => this.errorPuja = 'No se pudo modificar la puja.'
    });
  }

  puedeEditarPuja(oferta: Oferta): boolean {
    return !!oferta.disponibilidad && !oferta.partida && !oferta.destino;
  }

  mostrarRiven(id: string, event: MouseEvent): void {
    this.popupX = event.clientX;
    this.popupY = event.clientY;
    this.rivenService.getPorId(id).subscribe({
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
