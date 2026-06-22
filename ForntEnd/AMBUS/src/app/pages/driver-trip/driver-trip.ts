import { ChangeDetectorRef, Component } from '@angular/core';
import { AuthService } from '../../Core/Services/auth-service';
import { DriverService } from '../../Core/Services/driver-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { User } from '../../Core/Services/user';
import { ChatService } from '../../Core/Services/chat-service';

@Component({
  selector: 'app-driver-trip',
  imports: [FormsModule,CommonModule],
  templateUrl: './driver-trip.html',
  styleUrl: './driver-trip.css',
})
export class DriverTrip {
    status?:string="";
    AllTripss:any[]=[];
    message:string="";
userId:string="";

id:string="";
AllMessage:any[]=[]
    constructor(private _authService:AuthService
            ,private _driver :DriverService,
            private _cd :ChangeDetectorRef,
          private _chatservice:ChatService ,
          private _userService:User) {
                this.GetMyTrip();
                this._userService.GetProfile().subscribe({
                  next:(res) => {
                    this.userId = res.id;
                    this._cd.markForCheck();
                  }
                })

            }

    GetMyTrip()
    {
      this._driver.GetMyTrip(this.status).subscribe({
        next:(res)=>{
          this.AllTripss = res;
          this._cd.markForCheck();
        }
      })
    }

    UploadFile(id:string){
      this._driver.downloadManifest(id);
    }


 OpenConv(){
    this._chatservice.OpenConv().subscribe({
      next:(res) =>{
        this.id = res.id
        this.GetAllChat(res.id);
        this.closeModal();
      }
    });
  }

  GetAllChat(id:string){
    this._chatservice.GetMessages(id).subscribe({
      next:(res) =>{
        console.log(res);
        this.AllMessage = [...res.items];
        this._cd.markForCheck();
      }
    });
  }

  Send(){
    if(this.message == "") return;
    this._chatservice.SendMessage(this.id,this.message).subscribe({
      next:(res) =>{
        this.GetAllChat(this.id);
        this.message = "";
        this._cd.markForCheck();
      }
    });
  }

    closeModal(){
    document.getElementsByClassName('chat-popup')[0].classList.toggle("hide");
    document.getElementsByClassName('chat-bubble')[0].classList.toggle("hide");

  }

}
