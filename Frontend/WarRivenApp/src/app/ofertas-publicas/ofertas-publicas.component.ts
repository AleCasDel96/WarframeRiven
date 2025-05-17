import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OfertaService } from '../services/oferta.service';
import { RivenService } from '../services/riven.service';
import { Oferta } from '../models/oferta.model';
import { Riven } from '../models/riven.model';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgPipesModule } from 'ngx-pipes';
import { AuthService } from '../services/auth.service';

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

  filtroArma: string = '';
  armasDisponibles: string[] = [];

  rivenSeleccionado: Riven | null = null;
  popupX = 0;
  popupY = 0;
  showPopup = false;

  pujaOferta: Oferta | null = null;
  nuevaPuja = 0;
  errorPuja = '';

  nicknameActual: string | null = null;

  constructor(
    private ofertaService: OfertaService,
    private rivenService: RivenService,
    private auth: AuthService
  ) { }

  ngOnInit(): void {
    this.nicknameActual = this.auth.getNickname();
    this.ofertaService.getPublicas().subscribe({
      next: data => {
        this.ofertas = data;

        const armasSet = new Set(data.map(o => o.arma).filter(Boolean));
        this.armasDisponibles = Array.from(armasSet) as string[];
      },
      error: () => this.error = 'No se pudieron cargar las ofertas públicas.'
    });
  }

  abrirPuja(oferta: Oferta): void {
    if (!this.nicknameActual) return;

    this.rivenService.getPorId(oferta.idRiven).subscribe({
      next: (riven: Riven) => {
        const esDueño = riven.idUsuario === this.nicknameActual;
        const yaEsPujador = oferta.nickUsuario === this.nicknameActual;

        if (!esDueño && yaEsPujador) {
          this.error = 'No puedes pujar de nuevo si ya eres el pujador actual.';
          return;
        }

        this.pujaOferta = oferta;
        this.nuevaPuja = oferta.precioVenta || 0;
        this.errorPuja = '';
      },
      error: () => this.error = 'No se pudo verificar el propietario del Riven.'
    });
  }

  cerrarPuja(): void {
    this.pujaOferta = null;
    this.errorPuja = '';
  }

  confirmarPuja(): void {
    if (!this.pujaOferta || !this.nicknameActual) return;

    const id = this.pujaOferta?.id;
    if (!id) return;
    const precioActual = this.pujaOferta.precioVenta ?? 0;

    this.rivenService.getPorId(this.pujaOferta.idRiven).subscribe({
      next: (riven: Riven) => {
        const esDueño = riven.idUsuario === this.nicknameActual;

        if (!esDueño && this.nuevaPuja <= precioActual) {
          this.errorPuja = 'La puja debe ser mayor que el precio actual.';
          return;
        }

        this.ofertaService.editar(id, { precioVenta: this.nuevaPuja }).subscribe({
          next: () => {
            this.pujaOferta = null;
            this.ngOnInit();
          },
          error: () => this.errorPuja = 'No se pudo realizar la puja.'
        });
      },
      error: () => this.errorPuja = 'No se pudo validar el propietario del Riven.'
    });
  }

  puedePujar(oferta: Oferta): boolean {
    return (
      !!this.nicknameActual &&
      oferta.disponibilidad === true &&
      oferta.nickUsuario !== this.nicknameActual
    );
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
