import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from "@angular/router";

@Component({
  selector: 'app-auth-nav',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './auth-nav.html',
  styleUrl: './auth-nav.css',
})
export class AuthNav {

}
