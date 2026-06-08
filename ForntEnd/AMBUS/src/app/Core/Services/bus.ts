import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseUrl } from '../baseUrl';
import { IBus } from '../interfaces/i-bus';

@Injectable({
  providedIn: 'root',
})
export class Bus {
   constructor(private _httpClient:HttpClient){}

   GetAllBuses():Observable<any>{
     return this._httpClient.get(BaseUrl+"/Buses/GetAllBuses");
   }

   Search(Data:{type:string,isActive?:boolean}):Observable<any>
   {
     if(Data.isActive == null || Data.isActive == undefined ){
       return this._httpClient.get(BaseUrl+"/Buses?type="+Data.type);
     }
     return this._httpClient.get(BaseUrl+"/Buses?type="+Data.type+"&isActive="+Data.isActive);
   }
   GetBusById(id:string):Observable<any>{
     return this._httpClient.get(BaseUrl+"/Buses/"+id);
   }

   CreateBus(Data:IBus):Observable<any>{
     return this._httpClient.post(BaseUrl+"/Buses/create-bus",Data);
   }

   DeleteBus(id:string):Observable<any>{
     return this._httpClient.delete(BaseUrl+"/Buses/"+id);
   }

   UpdateBus(id:string, Data:{model:string,isActive:boolean,plateNumber:string}):Observable<any>
   {
     return this._httpClient.put(BaseUrl+"/Buses/"+id,Data);
   }

   GetSeats(id:string):Observable<any>{
     return this._httpClient.get(BaseUrl+"/Buses/"+id+"/seats");
   }


}
