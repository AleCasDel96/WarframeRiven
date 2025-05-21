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
    path: 'perfil',
    loadComponent: () => import('./perfil/perfil.component').then(m => m.PerfilComponent)
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
    path: 'historial',
    loadComponent: () => import('./historial/historial.component').then(m => m.HistorialComponent)
  },
  {
    path: 'rivens',
    loadComponent: () => import('./rivens/rivens.component').then(m => m.RivensComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];