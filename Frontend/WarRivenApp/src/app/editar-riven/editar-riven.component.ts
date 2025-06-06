import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { RivenService } from '../services/riven.service';
import { Riven } from '../models/riven.model';

@Component({
  selector: 'app-editar-riven',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './editar-riven.component.html'
})
export class EditarRivenComponent implements OnInit {
  riven: Partial<Riven> = {};
  error = '';
  id = '';

  constructor(
    private route: ActivatedRoute,
    private rivenService: RivenService,
    private router: Router
  ) {}

  ngOnInit(): void {
    console.log('a: ' + this.id);
    this.id = this.route.snapshot.paramMap.get('id') || '';
    console.log('b: ' + this.id);
    if (this.id) {
      this.rivenService.getPorId(this.id).subscribe({
        next: (r: Riven) => {
          this.riven = r;
          console.log(this.riven)
        },
        error: () => this.error = 'No se pudo cargar el Riven.'
      });
    }
  }

  actualizar(): void {
    if (!this.riven.nombre || !this.riven.arma || !this.riven.atrib1 || this.riven.valor1 === null) {
      this.error = 'Debes introducir nombre, arma y al menos una estadística válida.';
      return;
    }

    const cuerpo = { ...this.riven };

    // Elimina stats incompletos
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

    this.rivenService.editar(this.id, cuerpo).subscribe({
      next: () => this.router.navigate(['/rivens']),
      error: () => this.error = 'Error al actualizar el Riven.'
    });
  }
}
