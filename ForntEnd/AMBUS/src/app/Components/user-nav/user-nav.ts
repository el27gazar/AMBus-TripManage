import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from "@angular/router";

@Component({
  selector: 'app-user-nav',
  imports: [RouterLink,RouterLinkActive],
  templateUrl: './user-nav.html',
  styleUrl: './user-nav.css',
})
export class UserNav {

  tooglemenu(){
    document.getElementsByClassName("navbar-container")[0].classList.toggle("display");
    document.getElementsByClassName("container-list")[0].classList.toggle("display");
  }

}
