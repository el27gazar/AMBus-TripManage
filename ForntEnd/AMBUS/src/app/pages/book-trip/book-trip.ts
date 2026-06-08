import { IRoute } from './../../Core/interfaces/i-route';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit, viewChild, ViewEncapsulation } from '@angular/core';
import { TripService } from '../../Core/Services/trip-service';
import { DatePipe, CommonModule } from '@angular/common';
import { GlobalService } from '../../Core/Services/global-service';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Route } from '../../Core/Services/route';

@Component({
  selector: 'app-book-trip',
  imports: [ReactiveFormsModule, DatePipe, CommonModule],
  templateUrl: './book-trip.html',
  styleUrl: './book-trip.css',
})
export class BookTrip implements OnInit,AfterViewInit {
  Routes:IRoute[] = [];
  AllTrips:any[] = [];
  isLoaded:boolean = false;
  AllSeats :any[]=[];
  SelectedSeat:any[]=[];
  fromId!:FormControl;
  toId!:FormControl;
  date!:FormControl;

  FormSearch!:FormGroup;

  initFormControl(){
    this.fromId = new FormControl('');
    this.toId = new FormControl('');
    this.date = new FormControl('');
  }

  initFormGroup(){
    this.FormSearch = new FormGroup({
      fromId: this.fromId,
      toId: this.toId,
      date: this.date
    });
  }

  constructor(private _route:Route,
    private _cd:ChangeDetectorRef,
     private _trip:TripService,
    private _toast:GlobalService){
    this.initFormControl();
    this.initFormGroup();
    this.GetAllTrips();
  }

     ngOnInit() {
        this.GetAllRoutes();
    }


    ngAfterViewInit() {
    }

   GetAllRoutes() {
      this._route.GetAllRoutes().subscribe({
       next:(res)=>{
         this.Routes = [...res];
         this.isLoaded = true;
         this._cd.markForCheck();
       }
     })
   }
   GetAllTrips(){
    this._trip.GetAll().subscribe({
      next:(res)=>{
           this.AllTrips = [...res.items];
           this._cd.markForCheck();
      }
    })
   }
   search(){
    this._trip.Search({fromCity:this.FormSearch.value.fromId,toCity:this.FormSearch.value.toId,date:this.FormSearch.value.date}).subscribe({
      next:(res)=>{
        this.AllTrips = [...res.items];
        this._cd.markForCheck();
      },
       error:(err)=>{
         this._toast.showToaster(err.error.errors[0]);
       }
    });
   }

   SelectSeat(id:string){
     this._trip.GetSeats(id).subscribe({
       next:(res)=>{
         this.AllSeats = [...res];
         this._cd.markForCheck();
       }
     });
      this.toogle();
      this.SelectedSeat = [];
   }

   Select(seat:any){
     if(this.SelectedSeat.includes(seat))
     {
      this._toast.showToaster("Seat Already Selected");
      return;
     }
     if(this.SelectedSeat.length >= 5)
     {
      this._toast.showToaster("You Cannot Select More Than 5 Seats");
      return;
     }
     this.SelectedSeat.push(seat);
    document.getElementById(seat.seatNumber)?.classList.add("selectseat");
   }

   delete(seat:any){
     this.SelectedSeat.splice(this.SelectedSeat.indexOf(seat),1);
     document.getElementById(seat.seatNumber)?.classList.remove("selectseat");
   }

   toogle(){
      document.getElementsByClassName("container-Buses")[0].classList.toggle("hide");
     document.getElementsByClassName("containerSeat")[0].classList.toggle("hide");
   }

   

   Book(){

   }

}
