import { ChangeDetectorRef, Component } from '@angular/core';
import { AuthService } from '../../Core/Services/auth-service';
import { DriverService } from '../../Core/Services/driver-service';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLinkWithHref } from '@angular/router';
import { Footer } from "../../pages/footer/footer";

@Component({
  selector: 'app-driver-layout',
  imports: [CommonModule, RouterOutlet,  RouterLinkWithHref],
  templateUrl: './driver-layout.html',
  styleUrl: './driver-layout.css',
})
export class DriverLayout {
isOpen = false;
  AllTripss:any[]=[];
  constructor(private _authService:AuthService
          ,private _driver :DriverService,private _cd :ChangeDetectorRef ) { }


  GetMyTrip()
  {
    this._driver.GetMyTrip().subscribe({
      next:(res)=>{
        this.AllTripss = res;
        this._cd.markForCheck();
      }
    })
  }

  logout(){
    this._authService.Logout().subscribe({
      next:(res)=>{
        localStorage.removeItem('role');
        location.href = '/login';
      },
    });
  }




}
