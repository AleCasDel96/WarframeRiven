import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const noAuthGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = localStorage.getItem('jwt');

  if (token) {
    router.navigate(['/']);
    return false;
  }

  return true;
};
