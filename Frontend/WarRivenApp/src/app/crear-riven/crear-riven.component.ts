import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RivenService, Riven } from '../services/riven.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-crear-riven',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './crear-riven.component.html'
})
export class CrearRivenComponent {
  riven: any = {
    nombre: '',
    arma: '',
    stat1: '',
    valor1: null,
    stat2: '',
    valor2: null,
    stat3: '',
    valor3: null,
    stat4: '',
    valor4: null
  };
  error = '';

  constructor(private rivenService: RivenService, private router: Router) { }

  crear() {
    if (!this.riven.arma || !this.riven.stat1 || this.riven.valor1 === null) {
      this.error = 'Debes introducir el arma y el primer stat con su valor.';
      return;
    }

    const cuerpo = { ...this.riven };

    // Validación de parejas opcionales (stat2 - valor2, etc.)
    for (let i = 2; i <= 4; i++) {
      const statKey = `stat${i}` as keyof typeof cuerpo;
      const valorKey = `valor${i}` as keyof typeof cuerpo;

      const stat = cuerpo[statKey];
      const valor = cuerpo[valorKey];

      const statPresente = !!stat && stat.toString().trim() !== '';
      const valorPresente = valor !== null && valor !== undefined;

      if ((statPresente && !valorPresente) || (!statPresente && valorPresente)) {
        this.error = `La estadística ${i} está incompleta (falta texto o valor).`;
        return;
      }

      if (!statPresente) delete cuerpo[statKey];
      if (!valorPresente) delete cuerpo[valorKey];
    }

    this.rivenService.crearRiven(cuerpo).subscribe({
      next: () => this.router.navigate(['/rivens']),
      error: () => this.error = 'Error al crear el Riven'
    });
  }
}
