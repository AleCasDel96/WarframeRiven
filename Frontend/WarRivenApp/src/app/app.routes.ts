import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RivensComponent } from './rivens/rivens.component';
import { CrearRivenComponent } from './crear-riven/crear-riven.component';
import { EditarRivenComponent } from './editar-riven/editar-riven.component';
import { OfertasPublicasComponent } from './ofertas-publicas/ofertas-publicas.component';
import { CrearOfertaComponent } from './crear-oferta/crear-oferta.component';
import { MisOfertasComponent } from './mis-ofertas/mis-ofertas.component';
import { authGuard } from './guards/auth.guard';
import { InicioComponent } from './inicio/inicio.component';
import { PerfilComponent } from './perfil/perfil.component';
import { HistorialComponent } from './historial/historial.component';
import { MisVentasComponent } from './mis-ventas/mis-ventas.component';
import { noAuthGuard } from './guards/no-auth.guard';
import { RegistroComponent } from './registro/registro.component';

export const routes: Routes = [
  { path: '', component: InicioComponent },
  { path: 'login', component: LoginComponent, canActivate: [noAuthGuard] },
  { path: 'registro', component: RegistroComponent, canActivate: [noAuthGuard] },
  { path: 'rivens', component: RivensComponent, canActivate: [authGuard] },
  { path: 'crear-riven', component: CrearRivenComponent, canActivate: [authGuard] },
  { path: 'editar-riven/:id', component: EditarRivenComponent, canActivate: [authGuard] },
  { path: 'ofertas', component: OfertasPublicasComponent },
  { path: 'crear-oferta', component: CrearOfertaComponent, canActivate: [authGuard] },
  { path: 'mis-ofertas', component: MisOfertasComponent, canActivate: [authGuard] },
  { path: 'perfil', component: PerfilComponent, canActivate: [authGuard] },
  { path: 'ventas', component: HistorialComponent, canActivate: [authGuard] },
  { path: 'mis-compras', component: MisVentasComponent, canActivate: [authGuard] }
];
