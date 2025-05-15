import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfertaService, Oferta } from '../services/oferta.service';
import { FormsModule } from '@angular/forms';
import { NgPipesModule } from 'ngx-pipes';

@Component({
  selector: 'app-mis-pujas',
  standalone: true,
  imports: [CommonModule, FormsModule, NgPipesModule],
  templateUrl: './mis-pujas.component.html'
})
export class MisPujasComponent implements OnInit {
  pujas: Oferta[] = [];
  error = '';
  searchText = '';
  sortColumn = 'nombreRiven';
  sortAsc = true;
  p = 1;

  constructor(private ofertaService: OfertaService) { }

  confirmarCompra(id: string) {
    this.ofertaService.confirmarCompra(id).subscribe({
      next: () => this.ngOnInit(),
      error: () => this.error = 'No se pudo confirmar la compra.'
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

  ngOnInit(): void {
    this.ofertaService.getMisPujas().subscribe({
      next: data => this.pujas = data,
      error: () => this.error = 'No se pudieron cargar tus pujas.'
    });
  }
}
