import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from './services/auth.service';
import { MensajeService } from './services/mensaje.service';
import { CommonModule, NgIf } from '@angular/common';
import { SidebarAlertasComponent } from './sidebar-alertas/sidebar-alertas.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    NgIf
  ],
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  mensaje: string | null = null;

  constructor(
    public auth: AuthService,
    private router: Router,
    private mensajeService: MensajeService
  ) { }

  ngOnInit(): void {
    this.mensaje = this.mensajeService.get();
  }

  forzarLogout() {
    sessionStorage.setItem('forceLogout', '1');
    this.auth.logout();
  }

  get nickname(): string | null {
    return this.auth.getNickname();
  }
}
