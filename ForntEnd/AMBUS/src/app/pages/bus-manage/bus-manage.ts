import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Bus } from '../../Core/Services/bus';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IBus } from '../../Core/interfaces/i-bus';

@Component({
  selector: 'app-bus-manage',
  imports: [ReactiveFormsModule],
  templateUrl: './bus-manage.html',
  styleUrl: './bus-manage.css',
})
export class BusManage implements OnInit,AfterViewInit {
AllBuses:any[]=[];
isLoading:boolean = false;

plateNumber!:FormControl;
model!:FormControl;
totalSeats!:FormControl;
type!:FormControl;

CreateForm! :FormGroup;

initFormControl(){
  this.plateNumber = new FormControl('',[Validators.required]);
  this.model = new FormControl('',[Validators.required]);
  this.totalSeats = new FormControl('',[Validators.required,Validators.min(50),Validators.max(100)]);
  this.type = new FormControl('',[Validators.required]);
}

initFormGroup(){
  this.CreateForm = new FormGroup({
    plateNumber : this.plateNumber,
    model : this.model,
    totalSeats : this.totalSeats,
    type : this.type
  });
}

constructor(private _busService:Bus,private _cd:ChangeDetectorRef){
  this.initFormControl();
  this.initFormGroup();
}


ngOnInit(){
  this.GetAll();
}
ngAfterViewInit(){
  // this.GetAll();
}

GetAll(){
  this._busService.GetAllBuses().subscribe({
    next:(res)=>{
      this.AllBuses = res;
      this.isLoading = true;
      this._cd.detectChanges();
    },
    error:(err)=>{
      alert(err.error);
    }

  });
}
Delete(id:string)
{
  this._busService.DeleteBus(id).subscribe({
    next:(res)=>{
      this.AllBuses = this.AllBuses.filter(x=>x.id!=id);
    },
    error:(err)=>{
      alert(err.error);
    }
  })
}

Get(id:string){
  console.log(id);
  this._busService.GetBusById(id).subscribe(res=>{
    console.log(res);
  });
}

submit(){
  if(this.CreateForm.valid){
    this.Create(this.CreateForm.value);
  }
  else{
    this.CreateForm.markAllAsDirty();
  }
}

Create(Data:IBus){
  this._busService.CreateBus(Data).subscribe({
    next:(res)=>{
      this.GetAll();
    },
    error:(err)=>{
      alert(err.error);
    }
  });

}

closeModal(){
  document.getElementsByClassName("overlay")[0].classList.toggle("hide");
  document.getElementsByClassName("modal")[0].classList.toggle("hide");
}

}

