import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseUrl } from '../baseUrl';

@Injectable({
  providedIn: 'root',
})
export class ChatService {

  constructor(private _httpClient:HttpClient) {}

  GetAllChat(id:string): Observable<any> {
    return this._httpClient.get(BaseUrl+"/Chat/"+id+"/messages");
  }

  OpenConversation(): Observable<any> {
    return this._httpClient.post(BaseUrl+"/Chat",{subject:"Welcome",firstMessage:"Welcome To AMbus"});
  }

  sendMessage(id:string,content:string): Observable<any> {
   return this._httpClient.post(BaseUrl+"/Chat/"+id+"/messages",{content:content});
  }

  GetAllConversation(): Observable<any> {
    return this._httpClient.get(BaseUrl+"/Chat");
  }

}
