import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OfertaService } from '../services/oferta.service';
import { RivenService } from '../services/riven.service';
import { Oferta } from '../models/oferta.model';
import { Riven } from '../models/riven.model';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgPipesModule } from 'ngx-pipes';

@Component({
  selector: 'app-ofertas-publicas',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxPaginationModule, NgPipesModule],
  templateUrl: './ofertas-publicas.component.html'
})
export class OfertasPublicasComponent implements OnInit {
  ofertas: Oferta[] = [];
  searchText = '';
  sortColumn = 'precioVenta';
  sortAsc = true;
  p = 1;
  error = '';

  rivenSeleccionado: Riven | null = null;
  popupX = 0;
  popupY = 0;
  showPopup = false;

  constructor(
    private ofertaService: OfertaService,
    private rivenService: RivenService
  ) {}

  ngOnInit(): void {
    this.ofertaService.getPublicas().subscribe({
      next: data => this.ofertas = data,
      error: () => this.error = 'No se pudieron cargar las ofertas públicas.'
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
