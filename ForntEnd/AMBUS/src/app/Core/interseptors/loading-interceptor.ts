import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
 const _ngxspinner = inject(NgxSpinnerService);
   const clonedReq = req.clone({
    withCredentials: true
  });
  _ngxspinner.show();
  return next(clonedReq).pipe(finalize(() => {
    _ngxspinner.hide();
  }));
};
