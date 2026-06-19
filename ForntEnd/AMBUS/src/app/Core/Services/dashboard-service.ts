import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseUrl } from '../baseUrl';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {

  constructor(private _httpClient:HttpClient) { }


  Getstates():Observable<any>
  {
    return this._httpClient.get(BaseUrl+"/Dashboard/stats");
  }

  GetRevenue(from?:Date,to?:Date):Observable<any>
  {
    if(from == null || from ==undefined && to == null || to ==undefined){
       return this._httpClient.get(BaseUrl+"/Dashboard/revenue?groupBy=month");
    }else if(from == null || from ==undefined){
       return this._httpClient.get(BaseUrl+"/Dashboard/revenue?to="+to+"&groupBy=month");

    }else if(to == null || to ==undefined){
       return this._httpClient.get(BaseUrl+"/Dashboard/revenue?from="+from+"&groupBy=month");

    }else{
    return this._httpClient.get(BaseUrl+"/Dashboard/revenue?from="+from+"&to="+to+"&groupBy=month");
  }
  }

  GetPouplerRoutes():Observable<any>
  {
    return this._httpClient.get(BaseUrl+"/Dashboard/popular-routes");
  }

  GetUpComingTrips():Observable<any>{
    return this._httpClient.get(BaseUrl+"/Dashboard/upcoming-trips");
  }

}
