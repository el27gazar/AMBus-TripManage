import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { BaseUrl } from '../baseUrl';

@Injectable({
  providedIn: 'root',
})
export class ChatService {

  constructor(private _http: HttpClient) { }

  GetMyChat():Observable<any>{
    return this._http.get(BaseUrl+"/Chat/my")
  }

  GetChats():Observable<any>{
    return this._http.get(BaseUrl+"/Chat")
  }

  OpenConv(subject:string="Welcome",firstMessage:string="Hello"):Observable<any>{
    return this._http.post(BaseUrl+"/Chat",{subject,firstMessage})
  }

  SendMessage(id:string,content:string):Observable<any>{
    return this._http.post(BaseUrl+"/Chat/"+id+"/messages",{content})
  }

  GetMessages(id:string):Observable<any>{
    return this._http.get(BaseUrl+"/Chat/"+id+"/messages")
  }


}
