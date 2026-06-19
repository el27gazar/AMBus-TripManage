import { ChangeDetectorRef, Component } from '@angular/core';
import { ChatService } from '../../Core/Services/chat-service';
import { FormsModule } from '@angular/forms';
import { User } from '../../Core/Services/user';

@Component({
  selector: 'app-chat',
  imports: [FormsModule],
  templateUrl:'./chat.html',
  styleUrl: './chat.css',
})
export class Chat {
AllMessage:any[]=[]
userId:string=""
message:string=""
id:string=""
  AllConversation:any[]=[]
  constructor(private _chatservice:ChatService,
            private _cd:ChangeDetectorRef,
            private _userService:User
  ) {
    // this.GetAllConversation();
    this._userService.GetProfile().subscribe({
      next:(res) =>{
        this.userId = res.id;
        this._cd.markForCheck();
      }
    })
  }

  // GetAllConversation(){
  //  this._chatservice.GetAllConversation().subscribe({
  //   next:(res) =>{
  //     this.AllConversation = [...res.items];
  //     this._cd.markForCheck();
  //   }
  //  });
  // }

  // chat(id:string){
  //   this.id = id;
  //   console.log(id);
  //   this.GetAllChat(id);
  //    this.closeModal();
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
  }

}
