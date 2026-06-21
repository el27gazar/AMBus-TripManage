import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class ChatServiceTest {
   private hubConnection!: signalR.HubConnection;

  startConnection(token: string) {

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7000/chatHub', {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build();

    return this.hubConnection.start();
  }

  joinConversation(conversationId: string) {
    return this.hubConnection.invoke(
      'JoinConversation',
      conversationId
    );
  }

  leaveConversation(conversationId: string) {
    return this.hubConnection.invoke(
      'LeaveConversation',
      conversationId
    );
  }

  sendMessage(
    conversationId: string,
    content: string
  ) {
    return this.hubConnection.invoke(
      'SendMessage',
      conversationId,
      content
    );
  }

  typing(conversationId: string) {
    return this.hubConnection.invoke(
      'Typing',
      conversationId
    );
  }

  stopTyping(conversationId: string) {
    return this.hubConnection.invoke(
      'StopTyping',
      conversationId
    );
  }

  markAsRead(conversationId: string) {
    return this.hubConnection.invoke(
      'MarkAsRead',
      conversationId
    );
  }

  closeConversation(conversationId: string) {
    return this.hubConnection.invoke(
      'CloseConversation',
      conversationId
    );
  }

  reopenConversation(conversationId: string) {
    return this.hubConnection.invoke(
      'ReopenConversation',
      conversationId
    );
  }

  // Events

  onReceiveMessage(callback: any) {
    this.hubConnection.on(
      'ReceiveMessage',
      callback
    );
  }

  onTyping(callback: any) {
    this.hubConnection.on(
      'UserTyping',
      callback
    );
  }

  onStopTyping(callback: any) {
    this.hubConnection.on(
      'UserStoppedTyping',
      callback
    );
  }

  onMessagesRead(callback: any) {
    this.hubConnection.on(
      'MessagesRead',
      callback
    );
  }

  onConversationClosed(callback: any) {
    this.hubConnection.on(
      'ConversationClosed',
      callback
    );
  }

  onConversationReopened(callback: any) {
    this.hubConnection.on(
      'ConversationReopened',
      callback
    );
  }

  onError(callback: any) {
    this.hubConnection.on(
      'Error',
      callback
    );
  }
}
