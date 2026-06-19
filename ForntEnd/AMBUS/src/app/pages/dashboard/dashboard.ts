
import { ChangeDetectorRef, Component } from '@angular/core';
import { DashboardService } from '../../Core/Services/dashboard-service';
import {  FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-dashboard',
  imports: [ReactiveFormsModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
AllStatesKeys:any[]=[];
AllStatesValues:any;
totalRevenue:number=0
from!:FormControl
to!:FormControl

formRevenue!:FormGroup

AllPopular:any[]=[]

intiFormControl(){
  this.from=new FormControl('');
  this.to=new FormControl('');
}

initFormGroup(){
  this.formRevenue= new FormGroup({
    from:this.from,
    to:this.to
  });
}

   constructor(private _state:DashboardService,private _cd:ChangeDetectorRef) {
        this.GetStates();
        this. GetPopular();
        // this.GetRevenue();
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
    this._state.GetRevenue(this.formRevenue.value).subscribe({
      next:(res)=>{
       this.totalRevenue = res;
      }
    })
   }

   GetPopular(){
    this._state.GetPouplerRoutes().subscribe({
      next:(res)=>{
         this.AllPopular = [...res];
         this._cd.markForCheck();
      }
    })
   }
}

