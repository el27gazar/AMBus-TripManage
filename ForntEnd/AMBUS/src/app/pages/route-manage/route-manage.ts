import { ChangeDetectorRef, Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Route } from '../../Core/Services/route';
import { GlobalService } from '../../Core/Services/global-service';

@Component({
  selector: 'app-route-manage',
  imports: [ReactiveFormsModule],
  templateUrl: './route-manage.html',
  styleUrl: './route-manage.css',
})
export class RouteManage {
name!:FormControl;
AllRoutes:any[] =[];

updateName!:FormControl;
isActive!:FormControl;

updateForm! :FormGroup;

formGroup!:FormGroup;
constructor(private _route:Route,
  private _cd:ChangeDetectorRef,
 private _toast:GlobalService){}

intiformControl(){
  this.name = new FormControl('',[Validators.required]);
  this.updateName = new FormControl('',[Validators.required]);
  this.isActive = new FormControl('',[Validators.required]);
}

initFormGroup(){
  this.formGroup = new FormGroup({
    name : this.name
  });
  this.updateForm = new FormGroup({
    id : new FormControl('',[Validators.required]),
    updateName : this.updateName,
    isActive : this.isActive
  })
}

ngOnInit(){
 this.intiformControl();
  this.initFormGroup();
  this.GetAll();
}


GetAll()
{
   this._route.GetAllRoutes().subscribe({
    next:(res)=>{
      this.AllRoutes = [...res];
      this._cd.markForCheck();
    }
     ,
    error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
   }
 })
}

GetById(id:string){
  this._route.GetRouteById(id).subscribe({
    next:(res)=>{
     this.updateForm.get("id")?.setValue(res.id);
     this.updateForm.get("updateName")?.setValue(res.name);
     this.updateForm.get("isActive")?.setValue(res.isActive);
     this._toast.closeModal();
    },
    error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
     this._toast.closeModal();

    }
  })
}

Create()
{
  if(this.name.valid)
  {
    this._route.CreateRoute({name:this.name.value}).subscribe({
      next:(res)=>{
        this.GetAll();
        this._cd.markForCheck();
      },
      error:(err)=>{
        this._toast.showToaster(err.error.errors[0]);
         }
    })
  }else{
    this.name.markAllAsDirty();
  }
}


Delete(id:string)
{
  this._route.DeleteRoute(id).subscribe({
    next:(res)=>{
      this.GetAll();
      this._cd.markForCheck();
    },
    error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
    }
  })
}

search()
{

    this._route.Search({name:this.formGroup.value.name}).subscribe({
      next:(res)=>{
        this.AllRoutes = [...res];
        this._cd.markForCheck();
      },
      error:(err)=>{
        this._toast.showToaster(err.error.errors[0]);
      }
    })

}

Update(){
if(this.updateForm.valid)
{
  this._route.UpdateRoute(this.updateForm.value.id,{name:this.updateForm.value.updateName
    ,isActive:this.updateForm.value.isActive.toString()=="true"?true:false}).subscribe({
    next:(res)=>{
      this.GetAll();
      this._cd.markForCheck();
    },
    error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
    }
  })
}else{
  this.updateForm.markAllAsDirty();
}
}

closeModal(){
  this._toast.closeModal();
}
}
