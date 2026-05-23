import { Routes } from '@angular/router';

export const routes: Routes = [
  {path:'',loadComponent: () => import('./Layouts/auth-layout/auth-layout').then(m => m.AuthLayout)
    ,children:[
      {path:'',redirectTo:'home',pathMatch:'full'},
      {path:'home',loadComponent: () => import('./pages/home/home').then(m => m.Home)},
      {path:'login',loadComponent: () => import('./pages/login/login').then(m => m.Login)},
      {path:'register',loadComponent: () => import('./pages/register/register').then(m => m.Register)},
      {path:'ConfirmEmail',loadComponent: () => import('./pages/confirm-email/confirm-email').then(m => m.ConfirmEmail)},
      {path:'ResetPassword',loadComponent: () => import('./pages/reset-password/reset-password').then(m => m.ResetPassword)},
      {path:'ForgetPassword',loadComponent: () => import('./pages/forget-password/forget-password').then(m => m.ForgetPassword)},

    ]

  },
  {path:'user',loadComponent: () => import('./Layouts/user-layout/user-layout').then(m => m.UserLayout)
    ,
    children:[
      {path:'',redirectTo:'BookTrip',pathMatch:'full'},
      {path:'BookTrip',loadComponent: () => import('./pages/book-trip/book-trip').then(m => m.BookTrip)},
    ]

  }
];
