import { IRoute } from './../../Core/interfaces/i-route';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit, viewChild, ViewEncapsulation } from '@angular/core';
import { TripService } from '../../Core/Services/trip-service';
import { DatePipe, CommonModule } from '@angular/common';
import { GlobalService } from '../../Core/Services/global-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Route } from '../../Core/Services/route';
import { User } from '../../Core/Services/user';
import { BookTripService } from '../../Core/Services/book-trip';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-book-trip',
  imports: [ReactiveFormsModule, DatePipe, CommonModule, RouterOutlet],
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

  tripId!:string;

FormSearch!:FormGroup;
FormBook !:FormGroup;
tripid!:FormControl;
seats!:FormControl;
phoneNumber!:FormControl;
tripData:any;

MyData:any;


  initFormControl(){
    this.fromId = new FormControl('');
    this.toId = new FormControl('');
    this.date = new FormControl('');
    this.tripid = new FormControl('',[Validators.required]);
    this.phoneNumber = new FormControl('',[Validators.required,Validators.pattern('[0-9]{11}')]);
    this.seats = new FormControl('',[Validators.required]);

  }

  initFormGroup(){
    this.FormSearch = new FormGroup({
      fromId: this.fromId,
      toId: this.toId,
      date: this.date
    });

    this.FormBook = new FormGroup({
      tripId: this.tripid,
      PhoneNumber: this.phoneNumber,
      seats: this.seats
    })
  }

  constructor(private _route:Route,
    private _cd:ChangeDetectorRef,
     private _trip:TripService,
    private _toast:GlobalService,
    private _userService:User,
     private _book:BookTripService){
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
    this.tripId = id;
     this._trip.GetSeats(id).subscribe({
       next:(res)=>{
        // console.log(res);
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

   CheckBook(){
       if(this.SelectedSeat.length <=0)
       {
        this._toast.showToaster("Please Select Seats");
        return;
       }

       this._userService.GetProfile().subscribe({
       next:(res)=>{
         this.MyData = res;
         this.FormBook.get("PhoneNumber")?.setValue( this.MyData.phoneNumber);
         this._cd.markForCheck();
       }
     })
     this._trip.GetById(this.tripId).subscribe({
       next:(res)=>{
         this.tripData = res;
         this._cd.markForCheck();
       }
     })
   this.closeModal();

   }


   Book(){
     this.FormBook.get("tripId")?.setValue(this.tripId);
     this.FormBook.get("phoneNumber")?.setValue(this.MyData.phoneNumber);
     this.FormBook.get("seats")?.setValue(this.SelectedSeat.map(({seatNumber,isAvailable,...res})=>res));
     this._book.Create(this.FormBook.value).subscribe({
       next:(res)=>{
         location.href= res.checkoutUrl;
       },
       error:(err)=>{
         this._toast.showToaster(err.error.errors[0]);
       }
     });

   }

closeModal(){
  document.getElementsByClassName('modal')[0].classList.toggle("hide");
  document.getElementsByClassName('overlay')[0].classList.toggle("hide");
}
}
