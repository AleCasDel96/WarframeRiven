import { Routes } from '@angular/router';
import { noAuthGuard } from './guards/no-auth.guard';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./inicio/inicio.component').then(m => m.InicioComponent),
    canActivate: [noAuthGuard]
  },
  {
    path: 'login',
    loadComponent: () => import('./login/login.component').then(m => m.LoginComponent),
    canActivate: [noAuthGuard]
  },
  {
    path: 'registro',
    loadComponent: () => import('./registro/registro.component').then(m => m.RegistroComponent),
    canActivate: [noAuthGuard]
  },
  {
    path: 'perfil',
    loadComponent: () => import('./perfil/perfil.component').then(m => m.PerfilComponent),
    canActivate: [authGuard]
  },
  {
    path: 'crear-riven',
    loadComponent: () => import('./crear-riven/crear-riven.component').then(m => m.CrearRivenComponent),
    canActivate: [authGuard]
  },
  {
    path: 'editar-riven/:id',
    loadComponent: () => import('./editar-riven/editar-riven.component').then(m => m.EditarRivenComponent),
    canActivate: [authGuard]
  },
  {
    path: 'crear-oferta/:idRiven',
    loadComponent: () => import('./crear-oferta/crear-oferta.component').then(m => m.CrearOfertaComponent),
    canActivate: [authGuard]
  },
  {
    path: 'mercado',
    loadComponent: () => import('./mercado/mercado.component').then(m => m.MercadoComponent),
    canActivate: [authGuard]
  },
  {
    path: 'mis-ofertas',
    loadComponent: () => import('./mis-ofertas/mis-ofertas.component').then(m => m.MisOfertasComponent),
    canActivate: [authGuard]
  },
  {
    path: 'mis-ventas',
    loadComponent: () => import('./mis-ventas/mis-ventas.component').then(m => m.MisVentasComponent),
    canActivate: [authGuard]
  },
  {
    path: 'rivens',
    loadComponent: () => import('./rivens/rivens.component').then(m => m.RivensComponent),
    canActivate: [authGuard]
  },
  {
    path: '**',
    redirectTo: ''
  }
];