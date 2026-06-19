import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseUrl } from '../baseUrl';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class ChatService {

   private hubConnection!: signalR.HubConnection;
  private apiUrl = 'https://localhost:7172/api/chat';
  async startConnection() {

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7172/hubs/chat', {
        withCredentials: true
      })
      .withAutomaticReconnect()
      .build();

    return await this.hubConnection.start();
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

  onUserTyping(callback: any) {
    this.hubConnection.on(
      'UserTyping',
      callback
    );
  }

  onUserStoppedTyping(callback: any) {
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


   constructor(private http: HttpClient) {}

  getMyConversations(): Observable<any> {
    return this.http.get(`${this.apiUrl}/my`, {
      withCredentials: true
    });
  }

  getMessages(
    conversationId: string,
    page = 1,
    pageSize = 50
  ): Observable<any> {

    return this.http.get(
      `${this.apiUrl}/${conversationId}/messages?page=${page}&pageSize=${pageSize}`,
      {
        withCredentials: true
      }
    );
  }

  createConversation(
    subject: string,
    firstMessage: string
  ) {

    return this.http.post(
      this.apiUrl,
      {
        subject,
        firstMessage
      },
      {
        withCredentials: true
      }
    );
  }


}
