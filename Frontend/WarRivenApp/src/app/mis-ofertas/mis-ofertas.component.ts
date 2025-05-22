import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfertaService } from '../services/oferta.service';
import { RivenService } from '../services/riven.service';
import { Oferta } from '../models/oferta.model';
import { Riven } from '../models/riven.model';
import { Router, RouterModule } from '@angular/router';
import { NgPipesModule } from 'ngx-pipes';
import { NgxPaginationModule } from 'ngx-pagination';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-mis-ofertas',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NgxPaginationModule,
    NgPipesModule,
    RouterModule
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

  ofertaEditando: Oferta | null = null;
  nuevoPrecio: number = 0;
  errorEdicion: string = '';

  constructor(
    private ofertaService: OfertaService,
    private rivenService: RivenService,
    private router: Router,
    private http: HttpClient,
  ) { }

  ngOnInit(): void {
    this.obtenerOfertas();
  }

  obtenerOfertas(): void {
    this.ofertaService.getMisOfertas().subscribe({
      next: data => {
        console.log(data);
        this.ofertas = data;
        const armasSet = new Set(data.map(o => o.arma).filter(Boolean));
        this.armasDisponibles = Array.from(armasSet) as string[];
      },
      error: () => alert('Error al cargar tus ofertas')
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
    this.ofertaService.cambiarDisponibilidad(id).subscribe({
      next: () => this.obtenerOfertas(),
      error: () => alert('Error al cambiar disponibilidad')
    });
  }

  confirmarVenta(ofertaId: string): void {
    const confirmar = confirm('¿Deseas confirmar esta venta y traspasar el Riven al comprador?');
    if (!confirmar) return;

    this.http.get(`/api/Ventas/Venta/${ofertaId}`).subscribe({
      next: (venta: any) => {
        this.http.post('/api/Ventas/Confirmar', venta).subscribe({
          next: () => {
            alert('Venta confirmada y Riven traspasado.');
            this.obtenerOfertas(); // vuelve a cargar la lista
          },
          error: () => alert('Error al confirmar la venta.')
        });
      },
      error: () => alert('No se pudo obtener la venta.')
    });
  }


  eliminar(id: string): void {
    if (!confirm('¿Eliminar esta oferta?')) return;

    this.ofertaService.eliminar(id).subscribe({
      next: () => this.obtenerOfertas(),
      error: () => alert('Error al eliminar la oferta')
    });
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

  puedeEditar(oferta: Oferta): boolean {
    return oferta.disponibilidad!;
  }

  abrirEdicion(oferta: Oferta): void {
    this.ofertaEditando = oferta;
    this.nuevoPrecio = oferta.precioVenta;
    this.errorEdicion = '';
  }

  cerrarEdicion(): void {
    this.ofertaEditando = null;
    this.nuevoPrecio = 0;
    this.errorEdicion = '';
  }

  confirmarEdicion(): void {
    if (this.nuevoPrecio <= 0) {
      this.errorEdicion = 'El precio debe ser mayor a 0';
      return;
    }

    const nuevaOferta: Oferta = {
      ...this.ofertaEditando!,
      precioVenta: this.nuevoPrecio
    };

    this.ofertaService.editar(this.ofertaEditando?.id!, { precioVenta: this.nuevoPrecio }).subscribe({
      next: () => {
        alert('Oferta actualizada con éxito.');
        this.cerrarEdicion();
        this.obtenerOfertas();
      },
      error: () => {
        this.errorEdicion = 'Error al editar la oferta';
      }
    });
  }
}
