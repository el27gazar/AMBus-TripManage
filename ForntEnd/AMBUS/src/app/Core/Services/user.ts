import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseUrl } from '../baseUrl';

@Injectable({
  providedIn: 'root',
})
export class User {

  constructor(private _httpClient:HttpClient) { }

  GetAllUser(Data:{search:string,isActive?:boolean}):Observable<any>
  {
    if(Data.isActive == null || Data.isActive == undefined){
    return this._httpClient.get(BaseUrl+"/Users?search="+Data.search);
    }
    return this._httpClient.get(BaseUrl+"/Users?search="+Data.search+"&isActive="+Data.isActive);
  }


  GetProfile():Observable<any>
  {
    return this._httpClient.get(BaseUrl+"/Users/me");
  }

  GetUserById(id:string):Observable<any>
  {
    return this._httpClient.get(BaseUrl+"/Users/"+id);
  }

  UpdateUser(Data:{id:string,fullName:string,phoneNumber:string}):Observable<any>
  {
    return this._httpClient.put(BaseUrl+"/Users/"+Data.id,{fullName:Data.fullName,phoneNumber:Data.phoneNumber});
  }

  DeleteUser(id:string):Observable<any>
  {
    return this._httpClient.delete(BaseUrl+"/Users/"+id);
  }

  ActivatedUser(id:string):Observable<any>
  {
    return this._httpClient.put(BaseUrl+"/Users/"+id+"/activate","");
  }

  UpdateRole(id:string,role:string):Observable<any>
  {
    return this._httpClient.put(BaseUrl+"/Users/"+id+"/role",{role:role});
  }

}
