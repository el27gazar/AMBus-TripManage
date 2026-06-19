import { adminGuard } from './Core/Gards/admin-guard';
import { driverGuard } from './Core/Gards/driver-guard';
import { userGuard } from './Core/Gards/user-guard';
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
      // {path:'**',redirectTo:'home',pathMatch:'full'},
    ]

  },
  {path:'user',loadComponent: () => import('./Layouts/user-layout/user-layout').then(m => m.UserLayout)
    ,
    children:[
      {path:'',redirectTo:'home',pathMatch:'full'},
      {path:'home',loadComponent: () => import('./pages/home/home').then(m => m.Home),canActivate:[userGuard]},
      {path:'BookTrip',loadComponent: () => import('./pages/book-trip/book-trip').then(m => m.BookTrip),canActivate:[userGuard]
      },
      {path:'booking-success',loadComponent: () => import('./pages/booking-success/booking-success').then(m => m.BookingSuccess),canActivate:[userGuard]}
      ,
      {path:'MyTrips',loadComponent: () => import('./pages/my-trips/my-trips').then(m => m.MyTrips),canActivate:[userGuard]},
      {path:'About',loadComponent: () => import('./pages/about/about').then(m => m.About),canActivate:[userGuard]},
      {path:'Notification',loadComponent: () => import('./pages/notification/notification').then(m => m.Notification),canActivate:[userGuard]},
      {path:'Chat',loadComponent: () => import('./pages/chat-component/chat-component').then(m => m.ChatComponent),canActivate:[userGuard]},
      {path:'profile',loadComponent: () => import('./pages/profile/profile').then(m => m.Profile),canActivate:[userGuard]},
    ],
    canActivate:[userGuard]

  },
  {path:'admin',loadComponent: () => import('./Layouts/admin-layout/admin-layout').then(m => m.AdminLayout),
    children:[
      {path:'',redirectTo:'Dashboard',pathMatch:'full'},
      {path:'Dashboard',loadComponent: () => import('./pages/dashboard/dashboard').then(m => m.Dashboard),canActivate:[adminGuard]},
      {path:'userManagement',loadComponent: () => import('./pages/user-manage/user-manage').then(m => m.UserManage),canActivate:[adminGuard]},
      {path:'BusManagement',loadComponent: () => import('./pages/bus-manage/bus-manage').then(m => m.BusManage),canActivate:[adminGuard]},
      {path:'RoutesManagement',loadComponent: () => import('./pages/route-manage/route-manage').then(m => m.RouteManage),canActivate:[adminGuard]},
      {path:'TripManagement',loadComponent: () => import('./pages/trip-manage/trip-manage').then(m => m.TripManage),canActivate:[adminGuard]},
      {path:'BookManagement',loadComponent: () => import('./pages/book-management/book-management').then(m => m.BookManagement),canActivate:[adminGuard]},
      {path:'ReviewManagement',loadComponent: () => import('./pages/review-manage/review-manage').then(m => m.ReviewManage),canActivate:[adminGuard]},
      {path:'Chats' ,loadComponent: () => import('./pages/chat/chat').then(m => m.Chat),canActivate:[adminGuard]},
      {path:'Settings',loadComponent: () => import('./pages/settings/settings').then(m => m.Settings),canActivate:[adminGuard]},
    ],
    canActivate:[adminGuard]
  },
  {path:'driver',loadComponent: () => import('./Layouts/driver-layout/driver-layout').then(m => m.DriverLayout),
    canActivate:[driverGuard]},
   {path:'unauthorized',loadComponent: () => import('./pages/unauthorized/unauthorized').then(m => m.Unauthorized)},
  // {path:'**',redirectTo:'Login',pathMatch:'full'},


];
