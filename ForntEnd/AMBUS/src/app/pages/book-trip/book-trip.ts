import { IRoute } from './../../Core/interfaces/i-route';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Route } from '../../Core/Services/route';

@Component({
  selector: 'app-book-trip',
  imports: [],
  templateUrl: './book-trip.html',
  styleUrl: './book-trip.css',
})
export class BookTrip implements OnInit,AfterViewInit {
  Routes:IRoute[] = [];
  isLoaded:boolean = false;
  constructor(private _route:Route,private _cd:ChangeDetectorRef){
    // this.GetAllRoutes();
  }

     ngOnInit() {
        this.GetAllRoutes();
    }


    ngAfterViewInit() {
    // setTimeout(() => {
    //   this.GetAllRoutes();
    // })

    }

   GetAllRoutes() {
      this._route.GetAllRoutes().subscribe({
       next:(res)=>{
         this.Routes = res;
         this.isLoaded = true;
         this._cd.detectChanges();
       }
     })
   }

}
