import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RivenService, Riven } from '../services/riven.service';
import { Router } from '@angular/router';
import { OfertaService } from '../services/oferta.service';
import { FormsModule } from '@angular/forms';
import { NgPipesModule } from 'ngx-pipes';

@Component({
  selector: 'app-rivens',
  standalone: true,
  imports: [CommonModule, FormsModule, NgPipesModule],
  templateUrl: './rivens.component.html'
})
export class RivensComponent implements OnInit {
  rivens: Riven[] = [];
  error = '';

  searchText = '';
sortColumn = 'nombre';
sortAsc = true;
p = 1;

  constructor(
    private rivenService: RivenService,
    private ofertaService: OfertaService,
    private router: Router
  ) { }

  crearOferta(riven: Riven) {
    const precioStr = prompt(`¿Cuál es el precio para el Riven "${riven.nombre}"?`);
    const precio = parseFloat(precioStr || '');

    if (isNaN(precio) || precio <= 0) {
      alert('Precio inválido.');
      return;
    }

    this.ofertaService.crear({ idRiven: riven.id, precioVenta: precio }).subscribe({
      next: () => {
        alert('Oferta creada correctamente.');
        this.ngOnInit(); // refrescar la lista
      },
      error: err => {
        alert(err.error || 'Error al crear la oferta.');
      }
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
    this.rivenService.getMisRivens().subscribe({
      next: data => this.rivens = data,
      error: () => this.error = 'No se pudieron cargar los Rivens.'
    });
  }

  editar(id: string) {
    this.router.navigate(['/editar-riven', id]);
  }

  eliminar(id: string) {
    if (confirm('¿Deseas eliminar este Riven?')) {
      this.rivenService.eliminarRiven(id).subscribe({
        next: () => this.rivens = this.rivens.filter(r => r.id !== id),
        error: () => this.error = 'No se pudo eliminar el Riven.'
      });
    }
  }
}
