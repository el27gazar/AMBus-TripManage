import { Component } from '@angular/core';
import { UserNav } from "../../Components/user-nav/user-nav";
import { RouterOutlet } from '@angular/router';
import { Footer } from "../../pages/footer/footer";

@Component({
  selector: 'app-user-layout',
  imports: [UserNav, RouterOutlet, Footer],
  templateUrl: './user-layout.html',
  styleUrl: './user-layout.css',
})
export class UserLayout {

}
