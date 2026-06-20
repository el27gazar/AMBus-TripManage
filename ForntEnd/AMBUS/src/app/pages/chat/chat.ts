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
user:any;
message:string=""
id:string=""
  AllConversation:any[]=[]
  constructor(private _chatservice:ChatService,
            private _cd:ChangeDetectorRef,
            private _userService:User
  ) {
    this.GetAllConversation();
    this._userService.GetProfile().subscribe({
      next:(res) =>{
        this.userId = res.id;
        this._cd.markForCheck();
      }
    })
  }

  GetAllConversation(){
   this._chatservice.GetChats().subscribe({
    next:(res) =>{
      this.AllConversation = [...res.items];
      this._cd.markForCheck();
    }
   });
  }

  GetAllMessages(id:string){
    this.id = id;
    this._chatservice.GetMessages(id).subscribe({
      next:(res) =>{
        this.AllMessage = [...res.items];
        this.user = res.items[0].senderName;

        this._cd.markForCheck();
      }
    });
  }




  Send(){
    if(this.message == "") return;
    this._chatservice.SendMessage(this.id,this.message).subscribe({
      next:(res) =>{
        this.GetAllMessages(this.id);
      }
    });
  }

  closeModal(){
    document.getElementsByClassName('chat')[0].classList.toggle("hide");
  }

}
