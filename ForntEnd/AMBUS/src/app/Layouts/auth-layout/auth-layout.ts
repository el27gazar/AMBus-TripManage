import { Component } from '@angular/core';
import { AuthNav } from '../../Components/auth-nav/auth-nav';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-auth-layout',
  imports: [AuthNav,RouterOutlet],
  templateUrl: './auth-layout.html',
  styleUrl: './auth-layout.css',
})
export class AuthLayout {

  constructor() {
    var role = localStorage.getItem('role');
    if(role === 'User') location.href = '/user/home'
    else if(role === 'Driver') location.href = '/driver'
    else if(role === 'Admin') location.href = '/admin/Dashboard'


  }
}
