import { Component } from '@angular/core';
import { ChatService } from '../../Core/Services/chat-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-chat-component',
  imports: [FormsModule,CommonModule],
  templateUrl: './chat-component.html',
  styleUrl: './chat-component.css',
})
export class ChatComponent {
messages: any[] = [];
conversations: any[] = [];
message = '';
 messageText = '';
conversationId ='c033ac49-3e2c-4f7c-9154-c5b3cf136cfd';

isTyping = false;

  constructor(
    private chatService: ChatService
  ) {
  }

  async ngOnInit() {
await this.chatService.startConnection();

    this.chatService.onReceiveMessage(
      (msg:any) => {

        if (
          msg.conversationId ===
          this.conversationId
        ) {
          this.messages.push(msg);
        }

      }
    );

    this.loadConversations();
    await this.chatService.startConnection();

    await this.chatService.joinConversation(
      this.conversationId
    );

    this.chatService.onReceiveMessage(
      (msg: any) => {

        this.messages.push(msg);

        console.log(msg);
        console.log(this.messages);

      }
    );

    this.chatService.onUserTyping(
      () => {
        this.isTyping = true;
      }
    );

    this.chatService.onUserStoppedTyping(
      () => {
        this.isTyping = false;
      }
    );

    this.chatService.onMessagesRead(
      (data: any) => {
        console.log('Read', data);
      }
    );

    this.chatService.onError(
      (error: string) => {
        alert(error);
      }
    );
  }

  loadConversations() {
    this.chatService
      .getMyConversations()
      .subscribe({

        next: (res:any) => {

          this.conversations = res.items ?? res;

        }

      });
    }

  async openConversation(
    conversationId:string
  ) {

    this.conversationId =
      conversationId;

    await this.chatService.joinConversation(
      conversationId
    );

    this.chatService
      .getMessages(conversationId)
      .subscribe({

        next:(res:any)=>{

          this.messages =
            res.items ?? res;

        }

      });

    this.chatService.markAsRead(
      conversationId
    );
  }

  sendMessage() {

    if (!this.message.trim())
      return;

    this.chatService.sendMessage(
      this.conversationId,
      this.message
    );

    this.message = '';
  }

  typing() {
    this.chatService.typing(
      this.conversationId
    );

  }

  stopTyping() {

    this.chatService.stopTyping(
      this.conversationId
    );

  }

  markRead() {

    this.chatService.markAsRead(
      this.conversationId
    );

  }


}
