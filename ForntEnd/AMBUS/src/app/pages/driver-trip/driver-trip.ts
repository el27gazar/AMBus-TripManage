import { ChangeDetectorRef, Component } from '@angular/core';
import { AuthService } from '../../Core/Services/auth-service';
import { DriverService } from '../../Core/Services/driver-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-driver-trip',
  imports: [FormsModule,CommonModule],
  templateUrl: './driver-trip.html',
  styleUrl: './driver-trip.css',
})
export class DriverTrip {

    status?:string="";
    AllTripss:any[]=[];
    constructor(private _authService:AuthService
            ,private _driver :DriverService,private _cd :ChangeDetectorRef ) {
                this.GetMyTrip();
            }


    GetMyTrip()
    {
      this._driver.GetMyTrip(this.status).subscribe({
        next:(res)=>{
          this.AllTripss = res;
          this._cd.markForCheck();
        }
      })
    }

    UploadFile(id:string){
      this._driver.downloadManifest(id);
    }

  


}
