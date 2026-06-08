import { ActivatedRoute } from '@angular/router';
import { ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from '../../Core/Services/user';
import { IUser } from '../../Core/interfaces/iuser';
import { GlobalService } from '../../Core/Services/global-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-manage',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './user-manage.html',
  styleUrl: './user-manage.css',
})
export class UserManage implements OnInit {
 SerachInput!:FormControl;
 SelectValue!:FormControl;
 SearchForm!:FormGroup;

 fullName!:FormControl;
 phoneNumber!:FormControl;
 id!:FormControl;
 role!:FormControl;
 UpdateForm!:FormGroup;

 AllUsers:any[] = [];

  constructor(private _userservice:User
    ,private _cd:ChangeDetectorRef
   , private _toast:GlobalService) {
    this.initFormControl();
    this.initFormGroup();
  }

  ngOnInit() {
      this.submit();
  }


  initFormControl(){
    this.SerachInput = new FormControl('');
    this.SelectValue = new FormControl('');
    this.id = new FormControl('');
    this.role = new FormControl('');
    this.fullName = new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)]);
    this.phoneNumber = new FormControl('',[Validators.required,Validators.pattern('[0-9]{11}')]);
  }

  initFormGroup(){
    this.SearchForm = new FormGroup({
      SerachInput: this.SerachInput,
      SelectValue: this.SelectValue
    });

    this.UpdateForm = new FormGroup({
      fullName: this.fullName,
      phoneNumber: this.phoneNumber,
      id: this.id,
      role: this.role
    });
  }


  submit(){
     if(this.SearchForm.value.SelectValue == "all" || this.SearchForm.value.SelectValue == "" )
     {
        this._userservice.GetAllUser({search:this.SearchForm.value.SerachInput}).subscribe({
          next:(res)=>{
            this.AllUsers = [...res.items];
            this._cd.markForCheck();
          }

        });
      }else{
       this._userservice.GetAllUser({search:this.SearchForm.value.SerachInput,isActive:JSON.parse(this.SearchForm.value.SelectValue)}).subscribe({
          next:(res)=>{
          this.AllUsers = res.items;
          this._cd.detectChanges();
        }
      });
     }
  }

  Get(id:string){
    this._userservice.GetUserById(id).subscribe({
      next:(res)=>{
         this.fullName.setValue(res.fullName);
         this.phoneNumber.setValue(res.phoneNumber);
         this.role.setValue(res.role);
         this.id.setValue(res.id);
        this._toast.closeModal();
        this._cd.detectChanges();
      },
      error:(err)=>{
               this._toast.showToaster(err.error.errors[0]);
      }
    });
  }

  Ban(id:string,temp:boolean){
     if(temp){
       this.Delete(id);
     }else{
       this.Activated(id);
     }
  }


  Delete(id:string){
    this._userservice.DeleteUser(id).subscribe({
      next:(res)=>{
        this._toast.showToaster(res.message);
        this.submit();
      },
      error:(err)=>{
               this._toast.showToaster(err.error.errors[0]);

      }
    });
  }

  Activated(id:string){
    this._userservice.ActivatedUser(id).subscribe({
     next:(res)=>{
      this._toast.showToaster(res.message);
      this.submit();
     },
     error:(err)=>{
               this._toast.showToaster(err.error.errors[0]);
     }

    });
  }

  Update(){
    console.log(this.UpdateForm.value);
     if(this.UpdateForm.valid){
           this.UpdateRole(this.UpdateForm.value.id,this.UpdateForm.value.role);
       setTimeout(() => {
        this._userservice.UpdateUser({id:this.UpdateForm.value.id,fullName:this.UpdateForm.value.fullName,phoneNumber:this.UpdateForm.value.phoneNumber}).subscribe({
          next:(res)=>{
            this._toast.showToaster("User Updated Successfully");
            this._toast.closeModal();
            this.submit();
            this._cd.detectChanges();
          },
          error:(err)=>{
              this._toast.showToaster(err.error);
          }
        });

        },1000);
     }
     else{
      this.UpdateForm.markAllAsDirty();
     }
  }

  UpdateRole(id:string,role:string){
    this._userservice.UpdateRole(id,role).subscribe({
       next:(res)=>{
        // this._toast.showToaster(res.message);
       }
    })
  }
closeModal(){
  this._toast.closeModal();
}


}
