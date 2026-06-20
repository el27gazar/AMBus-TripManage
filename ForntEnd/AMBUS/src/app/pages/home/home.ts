import { ChatService } from './../../Core/Services/chat-service';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RouterLink } from "@angular/router";
import { Testimonials } from "../testimonials/testimonials";
import { StatisticsSection } from "../statistics-section/statistics-section";
import { User } from '../../Core/Services/user';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [RouterLink, Testimonials, StatisticsSection,  FormsModule],
  templateUrl:'./home.html',
  styleUrl: './home.css',
})
export class Home  {
isloggedIn: boolean = false;
userId:string="";
message:string="";
id:string="";
AllMessage:any[]=[]
  constructor(private userService:User,
    private _cd:ChangeDetectorRef,
      private _chatservice:ChatService) {
    this.LoggedIn();
   }


  LoggedIn() {
     this.userService.GetProfile().subscribe({
       next:(res) =>{
         if(res) this.isloggedIn = true;
         this.userId = res.id;
         this._cd.markForCheck();
       }
    });

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
    console.log(this.message);
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
