import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-user-nav',
  imports: [RouterLink,RouterLinkActive],
  templateUrl: './user-nav.html',
  styleUrl: './user-nav.css',
})
export class UserNav {

  constructor(private _cookieService:CookieService){}

  tooglemenu(){
    document.getElementsByClassName("navbar-container")[0].classList.toggle("display");
    document.getElementsByClassName("container-list")[0].classList.toggle("display");
  }

// jwt_token
  logout(){
     this._cookieService.delete("jwt_token","/");
     console.log(this._cookieService.get('jwt_token'));
  }


}
