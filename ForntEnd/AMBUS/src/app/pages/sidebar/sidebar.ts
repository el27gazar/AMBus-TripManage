import { NgClass } from '@angular/common';
import { Component, OnInit, AfterViewInit, inject } from '@angular/core';
import { RouterLink, RouterLinkActive } from "@angular/router";
import { AuthService } from '../../Core/Services/auth-service';

@Component({
  selector: 'app-sidebar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css',
})
export class Sidebar implements OnInit {

  show:boolean = true;

  _authservise = inject(AuthService);

  ngOnInit(){
     if(localStorage.getItem("show") == "false"){
       this.menu();
     }
  }
  AfterViewInit(){
    if(localStorage.getItem("show") == "false"){
      this.menu();
    }

  }


  logout(){
   this._authservise.Logout().subscribe({
    next:(res)=>{
      location.href="/login";
      localStorage.removeItem("role");
    },
   });
  }


  menu(){
     this.show = !this.show;
     localStorage.setItem("show",this.show.toString());
     document.getElementsByClassName("conatainer")[0].classList.toggle("width");
           document.getElementsByTagName("h1")[0].classList.toggle("none");
           document.getElementsByClassName("bi")[0].classList.toggle("bi-layout-sidebar-inset");

           var elements =  document.getElementsByTagName("span");
           for(var i = 0; i < elements.length; i++){
            elements[i].classList.toggle("none");
           }
  }




}
