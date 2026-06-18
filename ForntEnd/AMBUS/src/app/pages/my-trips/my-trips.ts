import { ChangeDetectorRef, Component } from '@angular/core';
import { BookTripService } from '../../Core/Services/book-trip';
import { CommonModule } from '@angular/common';
import { QRCodeComponent } from 'angularx-qrcode';
import { GlobalService } from '../../Core/Services/global-service';

@Component({
  selector: 'app-my-trips',
  imports: [CommonModule],
  templateUrl: './my-trips.html',
  styleUrl: './my-trips.css',
})
export class MyTrips {

  MyBooking:any[]=[];
  TicketData:any;
   constructor(private _bookService:BookTripService,
     private _cd:ChangeDetectorRef,
     private _toast:GlobalService
   ) {}

   ngOnInit(){
    this.GetMyBooking();
   }

   GetMyBooking(){
    this._bookService.GetMyBook().subscribe({
      next:(res)=>{
        this.MyBooking=[...res.items];
        this._cd.markForCheck();
      }
    })
   }

   GetTicket(id:string){
    this._bookService.GetTicket(id).subscribe({
      next:(res)=>{
        this.TicketData=res;
        this.closeModal();
        this._cd.markForCheck();
      }
    });
   }

closeModal(){
this._toast.closeModal();
}

}
