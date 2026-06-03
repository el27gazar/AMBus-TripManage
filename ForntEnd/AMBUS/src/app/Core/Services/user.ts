import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseUrl } from '../baseUrl';

@Injectable({
  providedIn: 'root',
})
export class User {

  constructor(private _httpClient:HttpClient) { }

  GetAllUser(Data:{search:string,isActive:boolean}):Observable<any>
  {
    return this._httpClient.get(BaseUrl+"/Users?search="+Data.search+"&isActive="+Data.isActive);
  }

  GetProfile():Observable<any>
  {
    return this._httpClient.get(BaseUrl+"/Users/me");
  }

  GetUserById(id:number):Observable<any>
  {
    return this._httpClient.get(BaseUrl+"/Users/"+id);
  }

  UpdateUser(Data:{id:string,fullName:string,phoneNumber:string}):Observable<any>
  {
    return this._httpClient.put(BaseUrl+"/Users/",Data);
  }

  DeleteUser(id:number):Observable<any>
  {
    return this._httpClient.delete(BaseUrl+"/Users/"+id);
  }


}
