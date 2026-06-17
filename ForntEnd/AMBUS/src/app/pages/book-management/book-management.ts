import { ChangeDetectorRef, Component } from '@angular/core';
import { BookTrip } from '../../Core/Services/book-trip';
import { GlobalService } from '../../Core/Services/global-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-book-management',
  imports: [CommonModule],
  templateUrl: './book-management.html',
  styleUrl: './book-management.css',
})
export class BookManagement {
AllBook:any[]=[]

DetailBook={
  id:" ",
  userName:" ",
  tripSummary:" ",
  status:" ",
  totalPrice:" ",
  qrCode:" ",
  bookedAt:new Date(),
  seats:[{seatNumber:" "}],
  payment:{
    method:" ",
    status:" "
  }
}

  constructor(private _bookService:BookTrip ,
               private _cd:ChangeDetectorRef,
               private _toast:GlobalService
  ) {
    this.GetAllBook();
  }


  GetAllBook(){
    this._bookService.GetAll().subscribe({
       next:(res)=>{
        this.AllBook=[...res.items];
        this._cd.markForCheck();
      }
    })
  }

  GetById(id:string){
    this._bookService.GetById(id).subscribe({
      next:(res)=>{
        this.DetailBook=res;
        this._cd.markForCheck();
      }
    })
  }


  OpenDetail(id:string){
    this.GetById(id);
    this._toast.closeModal();
  }
CloseModal(){
this._toast.closeModal();
}

Cancel(id:string){
  this._bookService.Cancel(id).subscribe({
    next:(res)=>{
      this.GetAllBook();
      this.CloseModal();
      this._cd.markForCheck();
    }
  })
}

Confirm(id:string){
  this._bookService.Confirm(id).subscribe({
    next:(res)=>{
      this.GetAllBook();
      this.CloseModal();
      this._cd.markForCheck();
    }
  })
}

}
