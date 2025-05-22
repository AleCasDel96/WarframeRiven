import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OfertaService } from '../services/oferta.service';
import { RivenService } from '../services/riven.service';
import { Oferta } from '../models/oferta.model';
import { Riven } from '../models/riven.model';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgPipesModule } from 'ngx-pipes';
import { HttpClient } from '@angular/common/http';
import { MensajeService } from '../services/mensaje.service';
import { AuthService } from '../services/auth.service';
import { log } from 'console';

@Component({
  selector: 'app-ofertas',
  standalone: true,
  imports: [CommonModule, FormsModule, NgxPaginationModule, NgPipesModule],
  templateUrl: './mercado.component.html'
})
export class MercadoComponent implements OnInit {
  ofertas: Oferta[] = [];
  // searchText = '';
  // sortColumn = 'precioVenta';
  // sortAsc = true;
  p = 1;
  error = '';

  filtroArma: string = '';
  armasDisponibles: string[] = [];

  rivenSeleccionado: Riven | null = null;
  popupX = 0;
  popupY = 0;
  showPopup = false;

  popupVisible = false;
  mensajeGenerado = '';

  nicknameActual: string | null = null;

  constructor(
    private ofertaService: OfertaService,
    private rivenService: RivenService,
    private auth: AuthService,
    private mensajeService: MensajeService,
    private http: HttpClient,
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

  // copiarMensaje(oferta: Oferta): void {
  //   if (oferta.disponibilidad) return;
  //   if (!oferta.nickUsuario || !oferta.nombreRiven) {
  //     this.mensajeService.set('Faltan datos para generar el mensaje.');
  //     return;
  //   }

  //   const mensaje = `/w ${oferta.nickUsuario} he visto tu riven ${oferta.nombreRiven} en WarframeRivens`;
  //   navigator.clipboard.writeText(mensaje).then(() => {
  //     this.mensajeService.set('Mensaje copiado al portapapeles.');
  //   }).catch(() => {
  //     this.mensajeService.set('No se pudo copiar el mensaje.');
  //   });
  // }

  mostrarPopup(oferta: Oferta): void {
    if (!oferta.nickUsuario || !oferta.nombreRiven) {
      this.mensajeService.set('Faltan datos para generar el mensaje.');
      return;
    }

    this.mensajeGenerado = `/w ${oferta.nickUsuario} he visto tu riven ${oferta.nombreRiven} en WarframeRivens`;
    this.popupVisible = true;
  }

  cerrarPopup(): void {
    this.popupVisible = false;
  }

  puedeComprar(oferta: any): boolean {
    return oferta.disponibilidad && oferta.idVendedor !== this.nicknameActual;
  }

  iniciarCompra(oferta: any): void {
    const confirmar = confirm(`¿Deseas comprar "${oferta.nombreRiven}" por ${oferta.precioVenta}p? Esto cerrará la oferta.`);
    if (!confirmar) return;
    console.log(oferta);
    this.http.post(`/api/Ventas/Vendido?ofertaId=${oferta.id}`, {})
      .subscribe({
        next: () => {
          alert('Compra registrada. Esperando confirmación del vendedor.');
          oferta.disponibilidad = false;
        },
        error: () => {
          alert('Error al registrar la compra.');
        }
      });
  }

  // ordenarPor(col: string): void {
  //   if (this.sortColumn === col) {
  //     this.sortAsc = !this.sortAsc;
  //   } else {
  //     this.sortColumn = col;
  //     this.sortAsc = true;
  //   }
  // }

  // mostrarRiven(idRiven: string, e: MouseEvent): void {
  //   this.popupX = e.clientX;
  //   this.popupY = e.clientY;
  //   this.rivenService.getPorId(idRiven).subscribe({
  //     next: (r: Riven) => {
  //       console.log(r);
  //       this.rivenSeleccionado = r;
  //       this.showPopup = true;
  //     },
  //     error: () => this.rivenSeleccionado = null
  //   });
  // }

  // ocultarRiven(): void {
  //   this.showPopup = false;
  // }
}
