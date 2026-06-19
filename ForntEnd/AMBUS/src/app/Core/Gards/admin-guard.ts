import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { User } from '../Services/user';
import { catchError, map, of } from 'rxjs';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(User);
  const router = inject(Router);
  if(localStorage.getItem('role') === 'Admin') return true

     return authService.GetProfile().pipe(
    map((user: any) => {
      if (user.role === 'Admin') {
        localStorage.setItem('role', user.role);
        console.log("inside");
        return true;
      }

      return router.createUrlTree(['/unauthorized']);
    }),
    catchError(() => {
      return of(router.createUrlTree(['/login']));
    })
  );

};
