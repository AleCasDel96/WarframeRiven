import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { MensajeService } from './services/mensaje.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const location = inject(Location);
  const mensajeService = inject(MensajeService);

  let token: string | null = null;
  if (typeof window !== 'undefined') {
    token = localStorage.getItem('jwt');
  }

  const authReq = token
    ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
    : req;

  return next(authReq).pipe(
    catchError(err => {
      if (err.status === 401) {
        router.navigate(['/login']);
      } else if (err.status === 403 && token) {
        mensajeService.set('No tienes permisos para acceder a esta página.');
        location.back();
      }
      return throwError(() => err);
    })
  );
};

