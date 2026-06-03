import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from "@angular/router";

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css',
})
export class Sidebar {


  menu(){
     document.getElementsByClassName("conatainer")[0].classList.toggle("width");
           document.getElementsByTagName("h1")[0].classList.toggle("none");
           document.getElementsByClassName("bi")[0].classList.toggle("bi-layout-sidebar-inset");

           var elements =  document.getElementsByTagName("span");
           for(var i = 0; i < elements.length; i++){
            elements[i].classList.toggle("none");
           }
  }

}
