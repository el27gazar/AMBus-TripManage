import { ResetPassword } from './../reset-password/reset-password';
import { AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Bus } from '../../Core/Services/bus';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IBus } from '../../Core/interfaces/i-bus';
import { GlobalService } from '../../Core/Services/global-service';

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
UpdateForm! :FormGroup;

searchType!:FormControl;
searchIsActive!:FormControl;
SearchForm! :FormGroup;


initFormControl(){
  this.plateNumber = new FormControl('',[Validators.required]);
  this.model = new FormControl('',[Validators.required]);
  this.totalSeats = new FormControl('',[Validators.required,Validators.min(50),Validators.max(100)]);
  this.type = new FormControl('',[Validators.required]);

  this.searchType = new FormControl('');
  this.searchIsActive = new FormControl('');
}

initFormGroup(){
  this.CreateForm = new FormGroup({
    plateNumber : this.plateNumber,
    model : this.model,
    totalSeats : this.totalSeats,
    type : this.type
  });

  this.UpdateForm = new FormGroup({
    id : new FormControl('',[Validators.required]),
    plateNumber : this.plateNumber,
    model : this.model,
    isActive: new FormControl(''),
  });

  this.SearchForm = new FormGroup({
    searchType : this.searchType,
    searchIsActive : this.searchIsActive
  });
}

constructor(private _busService:Bus,
  private _cd:ChangeDetectorRef,
    private _toast:GlobalService){
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
      this.AllBuses = [...res];
      this.isLoading = true;
      this._cd.markForCheck();
    },
    error:(err)=>{
        this._toast.showToaster(err.error.errors[0]);
    }

  });
}

Delete(id:string)
{
  this._busService.DeleteBus(id).subscribe({
    next:(res)=>{
      this.GetAll();
      this._cd.markForCheck();
    },
    error:(err)=>{
     this._toast.showToaster(err.error.errors[0]);
    }
  })
}

Get(id:string){
  console.log(id);
  this._busService.GetBusById(id).subscribe({
    next:(res)=>{
      this.UpdateForm.get("id")?.setValue(id);
      this.UpdateForm.get("plateNumber")?.setValue(res.plateNumber);
      this.UpdateForm.get("model")?.setValue(res.model);
      this.UpdateForm.get("isActive")?.setValue(res.isActive);
      this.closeUpdateModal();
    },
    error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
    }
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

Update(){
  console.log(this.UpdateForm.value);
  this._busService.UpdateBus(this.UpdateForm.value.id,
    {plateNumber:this.UpdateForm.value.plateNumber,model:this.UpdateForm.value.model,isActive:this.UpdateForm.value.isActive=="true"?true:false}).subscribe({
    next:(res)=>{
      this.GetAll();
      this._cd.markForCheck();
       this.closeUpdateModal();
    },
    error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
      console.log(err);
     this.closeUpdateModal();

    }
  });
}

Create(Data:IBus){
  this._busService.CreateBus(Data).subscribe({
    next:(res)=>{
      this.GetAll();
      this._cd.markForCheck();
      this._toast.closeModal();
    },
    error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
      this._toast.closeModal();
    }
  });

}

closeModal(){
  document.getElementsByClassName("overlay")[0].classList.toggle("hide");
  document.getElementsByClassName("modal")[0].classList.toggle("hide");
  document.getElementsByTagName("input")[0].focus();
   var elements = document.getElementsByTagName("input");
   for (var i = 0; i < elements.length; i++) {
     elements[i].value = '';
   }

}

closeUpdateModal(){
  document.getElementsByClassName("overlay")[0].classList.toggle("hide");
  document.getElementsByClassName("edit-modal")[0].classList.toggle("hide");
}

search(){
   console.log(this.SearchForm.value);
  this._busService.Search({type:this.SearchForm.value.searchType,isActive:this.SearchForm.value.searchIsActive=="all"?undefined:this.SearchForm.value.searchIsActive}).subscribe({
    next:(res)=>{
      this.AllBuses = [...res];
      this._cd.markForCheck();
    },
    error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
    }
  });
}

}
