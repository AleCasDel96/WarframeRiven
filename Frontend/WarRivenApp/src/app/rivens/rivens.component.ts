import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';

import { RivenService } from '../services/riven.service';
import { Riven } from '../models/riven.model';

import { NgxPaginationModule } from 'ngx-pagination';
import { NgPipesModule } from 'ngx-pipes';

@Component({
  selector: 'app-rivens',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, NgxPaginationModule, NgPipesModule],
  templateUrl: './rivens.component.html'
})
export class RivensComponent implements OnInit {
  rivens: Riven[] = [];
  searchText = '';
  sortColumn = 'nombre';
  sortAsc = true;
  p = 1;
  error = '';
  filtroArma: string = '';
  armasDisponibles: string[] = [];

  constructor(
    private rivenService: RivenService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.rivenService.getMisRivens().subscribe({
      next: data => {
        this.rivens = data;
        const armasSet = new Set(data.map(r => r.arma).filter(Boolean));
        this.armasDisponibles = Array.from(armasSet) as string[];
      },
      error: () => this.error = 'No se pudieron cargar los rivens.'
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

  editar(id: string): void {
    this.router.navigate(['/editar-riven', id]);
  }

  eliminar(id: string): void {
    if (confirm('¿Eliminar este Riven?')) {
      this.rivenService.eliminar(id).subscribe(() => this.cargar());
    }
  }

  crearOferta(riven: Riven): void {
    this.router.navigate(['/crear-oferta', riven.id]);
  }
}
