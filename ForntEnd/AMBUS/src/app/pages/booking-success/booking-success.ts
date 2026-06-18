import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-booking-success',
  imports: [],
  templateUrl: './booking-success.html',
  styleUrl: './booking-success.css',
})
export class BookingSuccess {

  constructor(private route: ActivatedRoute) {}
  ngOnInit() {
  this.route.queryParams.subscribe(params => {
    const sessionId = params['session_id'];

    console.log(sessionId);
  });
}
}
