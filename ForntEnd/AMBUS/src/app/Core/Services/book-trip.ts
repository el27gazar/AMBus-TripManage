import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseUrl } from '../baseUrl';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BookTrip {

  constructor(private _httpClient:HttpClient) { }


  GetAll():Observable<any>{
    return this._httpClient.get(BaseUrl+"/Bookings/all");
  }

  Create(Data:{tripId:string,seats:[{seatId:string}],phoneNumber:string}):Observable<any>{
  return this._httpClient.post(BaseUrl+"/Bookings",Data);
}

GetMyBook(status?:boolean):Observable<any>{
    if(status == null || status == undefined){
      return this._httpClient.get(BaseUrl+"/Bookings/my");
    }else{
      return this._httpClient.get(BaseUrl+"/Bookings/my?status="+status);
    }
}

GetById(id:string):Observable<any>{
  return this._httpClient.get(BaseUrl+"/Bookings/"+id);
}

GetTicket(id:string):Observable<any>{
  return this._httpClient.get(BaseUrl+"/Bookings/"+id+"/ticket");
}


Cancel(id:string):Observable<any>{
  return this._httpClient.put(BaseUrl+"/Bookings/"+id+"/cancel","");
}

Confirm(id:string):Observable<any>{
  return this._httpClient.put(BaseUrl+"/Bookings/"+id+"/confirm","");
}


}
