import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseUrl } from '../baseUrl';

@Injectable({
  providedIn: 'root',
})
export class DriverService {

  constructor(private _httpClient:HttpClient) {}

  GetAllDrivers(available?:boolean): Observable<any> {
    if(available == null || available == undefined)
      return this._httpClient.get(BaseUrl+"/Drivers");
    else
      return this._httpClient.get(BaseUrl+"/Drivers?available="+available);
  }

  Create(Data:{UserId:string,licenseNumber:string,licenseExpiry:Date,emergencyContact:string}): Observable<any> {
    console.log(Data);
    return this._httpClient.post(BaseUrl+"/Drivers",Data);
  }

  GetById(id:string): Observable<any> {
    return this._httpClient.get(BaseUrl+"/Drivers/"+id);
  }

  Update(id:string,Data:{licenseNumber:string,licenseExpiry:Date,emergencyContact:string}): Observable<any> {
    return this._httpClient.put(BaseUrl+"/Drivers/"+id,Data);
  }

  Delete(id:string): Observable<any> {
    return this._httpClient.delete(BaseUrl+"/Drivers/"+id);
  }

  Profile(id:string): Observable<any> {
    return this._httpClient.get(BaseUrl+"/Drivers/"+id+"/profile");
  }

}
