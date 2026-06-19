import { ChatService } from './../../Core/Services/chat-service';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { RouterLink } from "@angular/router";
import { Testimonials } from "../testimonials/testimonials";
import { StatisticsSection } from "../statistics-section/statistics-section";
import { Footer } from "../footer/footer";
import { User } from '../../Core/Services/user';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [RouterLink, Testimonials, StatisticsSection, FormsModule],
  templateUrl:'./home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
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


   async ngOnInit() {



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

  // OpenConv(){
  //   this._chatservice.OpenConversation().subscribe({
  //     next:(res) =>{
  //       this.id = res.id
  //       this.closeModal();
  //       this.GetAllChat(res.id);
  //     }
  //   });
  // }

  // GetAllChat(id:string){
  //   this._chatservice.GetAllChat(id).subscribe({
  //     next:(res) =>{
  //       this.AllMessage = [...res.items];
  //       this._cd.markForCheck();
  //     }
  //   });
  // }

  // Send(){
  //   console.log(this.message);
  //   if(this.message == "") return;
  //   this._chatservice.sendMessage(this.id,this.message).subscribe({
  //     next:(res) =>{
  //       this.GetAllChat(this.id);
  //     }
  //   });
  // }

  closeModal(){
    document.getElementsByClassName('chat')[0].classList.toggle("hide");
    document.getElementsByClassName('chat-bubble')[0].classList.toggle("hide");

  }
}
