import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookTripService } from '../../Core/Services/book-trip';

@Component({
  selector: 'app-booking-success',
  imports: [],
  templateUrl: './booking-success.html',
  styleUrl: './booking-success.css',
})
export class BookingSuccess {
sessionId:string=''
constructor(private route: ActivatedRoute,private _bookService:BookTripService) {}
  ngOnInit() {
  this.route.queryParams.subscribe(params => {
    this.sessionId = params['session_id'];
  });
}

CloseModal(){
  document.getElementsByClassName('overlay')[0].classList.toggle("hide");
  document.getElementsByClassName('modal')[0].classList.toggle("hide");
  location.href = "user/MyTrips";
}

Confirm(){
  this._bookService.ConfirmPayment(this.sessionId).subscribe({
    next:(res)=>{
      this.CloseModal();
    }
  })
}
}
