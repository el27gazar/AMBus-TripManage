import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ChatServiceTest } from '../../Core/Services/chat-service-test';
import { ChatMessageDto } from '../../Core/interfaces/chat-message-dto';

@Component({
  selector: 'app-chat-component',
  imports: [FormsModule,CommonModule],
  templateUrl: './chat-component.html',
  styleUrl: './chat-component.css',
})
export class ChatComponent {
messages: any[] = [];
message:any;
isTyping = false;
currentUserId:string='';
conversationId =
'7aafeb14-6b8e-4ec8-8f5e-4d6c77d96d10';
 chatService = inject(ChatServiceTest);
ngOnInit() {

  const token =
    localStorage.getItem('token')!;

  this.chatService
    .startConnection(token)
    .then(() => {

      this.chatService
        .joinConversation(this.conversationId);

      this.listenEvents();
    });
}

listenEvents() {

  this.chatService.onReceiveMessage(
    (message:ChatMessageDto) => {

      this.messages.push(message);

      this.chatService.markAsRead(
        this.conversationId
      );
    }
  );

  this.chatService.onTyping(() => {
    this.isTyping = true;
  });

  this.chatService.onStopTyping(() => {
    this.isTyping = false;
  });

  this.chatService.onError((err:any) => {
    alert(err);
  });
}

send() {

  if (!this.message.trim()) return;

  this.chatService.sendMessage(
    this.conversationId,
    this.message
  );

  this.message = '';
}



}
