import { Component } from '@angular/core';
import { AuthService } from '../../Core/Services/auth-service';

@Component({
  selector: 'app-driver-layout',
  imports: [],
  templateUrl: './driver-layout.html',
  styleUrl: './driver-layout.css',
})
export class DriverLayout {

  constructor(private _authService:AuthService) { }



  logout(){
    this._authService.Logout().subscribe({
      next:(res)=>{
        localStorage.removeItem('role');
        location.href = '/login';
      },
    });
  }
}
