
import { ChangeDetectorRef, Component } from '@angular/core';
import { DashboardService } from '../../Core/Services/dashboard-service';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
AllStatesKeys:any[]=[];
AllStatesValues:any;

   constructor(private _state:DashboardService,private _cd:ChangeDetectorRef) {
        this.GetStates();
    }

   GetStates(){
      this._state.Getstates().subscribe({
         next:(res)=>{
           this.AllStatesValues=res;
           this.AllStatesKeys=[...Object.keys(res)];
           this._cd.markForCheck();

         }
      });
   }

   GetRevenue(){
    
   }

}

