import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { AuthService } from '../../Core/Services/auth-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-nav',
  imports: [RouterLink,RouterLinkActive,CommonModule],
  templateUrl: './user-nav.html',
  styleUrl: './user-nav.css',
})
export class UserNav {

  isOpen = false;
  constructor(private _authService:AuthService){}

  tooglemenu(){
    document.getElementsByClassName("navbar-container")[0].classList.toggle("display");
    document.getElementsByClassName("container-list")[0].classList.toggle("display");
  }


  logout(){
     this._authService.Logout().subscribe(res=>{});
     this.isOpen=false;
  }


}
