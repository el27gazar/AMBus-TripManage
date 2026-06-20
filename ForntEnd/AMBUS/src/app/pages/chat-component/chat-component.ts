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





}
