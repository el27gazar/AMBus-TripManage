import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { User } from '../Services/user';
import { catchError, map, of } from 'rxjs';

export const driverGuard: CanActivateFn = (route, state) => {
const authService = inject(User);
  const router = inject(Router);
  if(localStorage.getItem('role') === 'Driver') return true

     return authService.GetProfile().pipe(
    map((user: any) => {
      if (user.role === 'Driver') {
        localStorage.setItem('role', user.role);
        return true;
      }

      return router.createUrlTree(['/unauthorized']);
    }),
    catchError(() => {
      return of(router.createUrlTree(['/login']));
    })
  );
};
