import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseUrl } from '../baseUrl';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {

  constructor(private _httpClient:HttpClient) {}


  GetAll():Observable<any>{
    return this._httpClient.get(BaseUrl+"/Notifications");
  }

  MakeRead(id:string):Observable<any>
  {
    return this._httpClient.put(BaseUrl+"/Notifications/"+id+"/read","");
  }

  MakeAllRead():Observable<any>{
    return this._httpClient.put(BaseUrl+"/Notifications/read-all","");
  }

  Delete(id:string):Observable<any>{
    return this._httpClient.delete(BaseUrl+"/Notifications/"+id);
  }

  Create(Data:{userIds:string[],type:string,message:string}):Observable<any>{
    return   this._httpClient.post(BaseUrl+"/Notifications",Data);
  }


}
