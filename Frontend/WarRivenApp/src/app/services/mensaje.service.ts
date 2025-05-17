import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class MensajeService {
  private mensaje = '';

  set(mensaje: string): void {
    this.mensaje = mensaje;
  }

  get(): string {
    const temp = this.mensaje;
    this.mensaje = ''; // limpiar tras obtener
    return temp;
  }
}

