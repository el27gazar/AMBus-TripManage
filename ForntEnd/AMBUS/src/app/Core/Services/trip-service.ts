import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseUrl } from '../baseUrl';
import { Observable } from 'rxjs';
import { ITrip } from '../interfaces/itrip';

@Injectable({
  providedIn: 'root',
})
export class TripService {

  constructor(private _httpClient:HttpClient) { }


  GetAll():Observable<any>{
    return this._httpClient.get(BaseUrl+"/Trips/GetAllTrips");
  }
  GetById(id:string):Observable<any>{
    return this._httpClient.get(BaseUrl+"/Trips/"+id);
  }

  Create(Data:ITrip):Observable<any>{
    console.log(Data);
    return this._httpClient.post(BaseUrl+"/Trips",Data);
  }

  Delete(id:string):Observable<any>{
    return this._httpClient.delete(BaseUrl+"/Trips/"+id);
  }

  Update(id:string,Data:{driverId:string,departureTime:Date,arrivalTime:Date,basePrice:number}):Observable<any>{
     console.log(Data);
    return this._httpClient.put(BaseUrl+"/Trips/"+id,Data);
  }

  Search(Data:{fromCity:string,toCity:string,date:Date}):Observable<any>{
    return this._httpClient.get(BaseUrl+"/Trips?fromCity="+Data.fromCity+"&toCity="+Data.toCity+"&date="+Data.date);
  }

  GetSeats(id:string):Observable<any>{
    return this._httpClient.get(BaseUrl+"/Trips/"+id+"/seats");
  }

}
