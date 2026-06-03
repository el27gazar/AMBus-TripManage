import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseUrl } from '../baseUrl';

@Injectable({
  providedIn: 'root',
})
export class Route {
  constructor(private _httpClient:HttpClient){}
  GetAllRoutes():Observable<any>{
     return this._httpClient.get(BaseUrl+"/Routes/GetAllRoutes");
  }

  CreateRoute(Data:{name:string}):Observable<any>{
     return this._httpClient.post(BaseUrl+"/Routes",Data);
  }

  GetRouteById(id:number):Observable<any>{
     return this._httpClient.get(BaseUrl+"/Routes/"+id);
  }

  UpdateRoute(id:number,Data:{name:string,isActive:boolean}):Observable<any>{
     return this._httpClient.put(BaseUrl+"/Routes/"+id,Data);
  }

  DeleteRoute(id:number):Observable<any>{
     return this._httpClient.delete(BaseUrl+"/Routes/"+id);
  }

}
