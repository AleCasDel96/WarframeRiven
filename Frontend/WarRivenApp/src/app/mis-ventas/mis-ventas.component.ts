import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { VentaService } from '../services/venta.service';
import { RivenService } from '../services/riven.service';
import { Riven } from '../models/riven.model';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgPipesModule } from 'ngx-pipes';
import { MensajeService } from '../services/mensaje.service';
import { Venta } from '../models/venta.model';
import { VentaDTO } from '../models/ventaDTO.model';

@Component({
  selector: 'app-mis-ventas',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxPaginationModule, NgPipesModule],
  templateUrl: './mis-ventas.component.html'
})
export class MisVentasComponent implements OnInit {
  Ventas: VentaDTO[] = [];
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
    this.ventaService.getMisVentas().subscribe({
      next: data => {
        this.Ventas = data;
        console.log(this.Ventas);
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

  confirmarVenta(id: string): void {
    this.ventaService.confirmarVenta(id).subscribe(() => this.ngOnInit());
  }

  eliminar(id: string): void {
    this.ventaService.eliminarVenta(id).subscribe(()=> this.ngOnInit())
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
