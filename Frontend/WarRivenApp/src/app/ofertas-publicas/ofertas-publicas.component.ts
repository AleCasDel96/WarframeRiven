import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfertaService, Oferta } from '../services/oferta.service';
import { RivenService, Riven } from '../services/riven.service';
import { FormsModule } from '@angular/forms';
import { NgPipesModule } from 'ngx-pipes';

@Component({
  selector: 'app-ofertas-publicas',
  standalone: true,
  imports: [CommonModule, FormsModule, NgPipesModule],
  templateUrl: './ofertas-publicas.component.html'
})
export class OfertasPublicasComponent implements OnInit {
  ofertas: Oferta[] = [];
  error = '';
  rivenSeleccionado: Riven | null = null;
  popupX = 0;
  popupY = 0;
  showPopup = false;
  searchText = '';
  sortColumn = 'nombreRiven';
  sortAsc = true;
  p = 1;

  constructor(private ofertaService: OfertaService, private rivenService: RivenService) { }

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

  ordenarPor(col: string) {
    if (this.sortColumn === col) {
      this.sortAsc = !this.sortAsc;
    } else {
      this.sortColumn = col;
      this.sortAsc = true;
    }
  }

  ocultarRiven() {
    this.showPopup = false;
  }

  ngOnInit(): void {
    this.ofertaService.getPublicas().subscribe({
      next: data => this.ofertas = data,
      error: () => this.error = 'No se pudieron cargar las ofertas.'
    });
  }
}
