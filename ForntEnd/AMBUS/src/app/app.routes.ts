import { Notification } from './pages/notification/notification';
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
      {path:'',redirectTo:'home',pathMatch:'full'},
      {path:'home',loadComponent: () => import('./pages/home/home').then(m => m.Home)},
      {path:'BookTrip',loadComponent: () => import('./pages/book-trip/book-trip').then(m => m.BookTrip)},
      {path:'MyTrips',loadComponent: () => import('./pages/my-trips/my-trips').then(m => m.MyTrips)},
      {path:'About',loadComponent: () => import('./pages/about/about').then(m => m.About)},
      {path:'Notification',loadComponent: () => import('./pages/notification/notification').then(m => m.Notification)},
    ]

  },
  {path:'admin',loadComponent: () => import('./Layouts/admin-layout/admin-layout').then(m => m.AdminLayout),
    children:[
      {path:'',redirectTo:'Dashboard',pathMatch:'full'},
      {path:'Dashboard',loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard)},
      {path:'userManagement',loadComponent: () => import('./pages/user-manage/user-manage').then(m => m.UserManage)},
      {path:'BusManagement',loadComponent: () => import('./pages/bus-manage/bus-manage').then(m => m.BusManage)},
      {path:'RoutesManagement',loadComponent: () => import('./pages/route-manage/route-manage').then(m => m.RouteManage)},
      {path:'TripManagement',loadComponent: () => import('./pages/trip-manage/trip-manage').then(m => m.TripManage)},
      {path:'BookManagement',loadComponent: () => import('./pages/book-management/book-management').then(m => m.BookManagement)},
      {path:'ReviewManagement',loadComponent: () => import('./pages/review-manage/review-manage').then(m => m.ReviewManage)},
      {path:'Chats' ,loadComponent: () => import('./pages/chat/chat').then(m => m.Chat)},
      {path:'Settings',loadComponent: () => import('./pages/settings/settings').then(m => m.Settings)},
    ]
  }
];
