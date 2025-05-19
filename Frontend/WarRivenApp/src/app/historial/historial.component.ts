import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { VentaService } from '../services/venta.service';
import { RivenService } from '../services/riven.service';
import { Venta } from '../models/venta.model';
import { Riven } from '../models/riven.model';

import { NgxPaginationModule } from 'ngx-pagination';
import { NgPipesModule } from 'ngx-pipes';

@Component({
  selector: 'app-historial',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxPaginationModule, NgPipesModule],
  templateUrl: './historial.component.html'
})
export class HistorialComponent implements OnInit {
  ventas: Venta[] = [];
  searchText = '';
  sortColumn = 'fechaVenta';
  sortAsc = false;
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
    private rivenService: RivenService
  ) { }

  ngOnInit(): void {
    this.ventaService.getMisVentas().subscribe({
      next: data => {
        this.ventas = data;

        const armasSet = new Set(data.map(o => o.arma).filter(Boolean));
        this.armasDisponibles = Array.from(armasSet) as string[];
      },
      error: () => this.error = 'No se pudo cargar el historial de ventas.'
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

  
}
