import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./inicio/inicio.component').then(m => m.InicioComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'registro',
    loadComponent: () => import('./registro/registro.component').then(m => m.RegistroComponent)
  },
  {
    path: 'crear-riven',
    loadComponent: () => import('./crear-riven/crear-riven.component').then(m => m.CrearRivenComponent)
  },
  {
    path: 'editar-riven/:id',
    loadComponent: () => import('./editar-riven/editar-riven.component').then(m => m.EditarRivenComponent)
  },
  {
    path: 'crear-oferta/:idRiven',
    loadComponent: () => import('./crear-oferta/crear-oferta.component').then(m => m.CrearOfertaComponent)
  },
  {
    path: 'ofertas-publicas',
    loadComponent: () => import('./ofertas-publicas/ofertas-publicas.component').then(m => m.OfertasPublicasComponent)
  },
  {
    path: 'mis-ofertas',
    loadComponent: () => import('./mis-ofertas/mis-ofertas.component').then(m => m.MisOfertasComponent)
  },
  {
    path: 'mis-ventas',
    loadComponent: () => import('./mis-ventas/mis-ventas.component').then(m => m.MisVentasComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];