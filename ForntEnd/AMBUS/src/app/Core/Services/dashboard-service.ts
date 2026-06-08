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

  GetRevenue():Observable<any>
  {
    return this._httpClient.get(BaseUrl+"/Dashboard/revenue");
  }

  GetPouplerRoutes():Observable<any>
  {
    return this._httpClient.get(BaseUrl+"/Dashboard/popular-routes");
  }

  GetUpComingTrips():Observable<any>{
    return this._httpClient.get(BaseUrl+"/Dashboard/upcoming-trips");
  }

}
