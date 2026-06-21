import { ChangeDetectorRef, Component } from '@angular/core';
import { User } from '../../Core/Services/user';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../../Core/Services/auth-service';
import { GlobalService } from '../../Core/Services/global-service';

@Component({
  selector: 'app-profile',
  imports: [ReactiveFormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile {
  user = {
  fullName: 'User',
  email: '8521d9cdbc@emailinbo.live',
  phoneNumber: '01235458966'
};
MyData:any;
fullName!:FormControl;
phoneNumber!:FormControl;
FormData!:FormGroup;

currentPassword!:FormControl;
newPassword!:FormControl;
confirmPassword!:FormControl;
FormChangePassword!:FormGroup;


initformControl(){
  this.fullName = new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)]);
  this.phoneNumber = new FormControl('',[Validators.required,Validators.pattern('[0-9]{11}')]);
  this.currentPassword = new FormControl('',[Validators.required]);
  this.newPassword = new FormControl('',[Validators.required,Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]);
  this.confirmPassword = new FormControl('',[Validators.required,this.ConfirmPasswordMatch(this.newPassword)]);
}

initFormGroup(){
  this.FormData = new FormGroup({
    fullName : this.fullName,
    phoneNumber : this.phoneNumber
  });

  this.FormChangePassword = new FormGroup({
    currentPassword : this.currentPassword,
    newPassword : this.newPassword,
    confirmPassword : this.confirmPassword
  });

}


constructor(private _userservice:User
  ,private _cd:ChangeDetectorRef,
   private _authservice :AuthService,
  private _toast:GlobalService){
  this.initformControl();
  this.initFormGroup();
  this.GetProfile();
}

GetProfile(){
  this._userservice.GetProfile().subscribe({
    next:(res)=>{
      this.MyData = res;
      this.FormData.get("fullName")?.setValue(this.MyData.fullName);
      this.FormData.get("phoneNumber")?.setValue(this.MyData.phoneNumber);
      this._cd.markForCheck();
    }
  });
}

Update(){
  if(this.FormData.valid){
    this._userservice.UpdateUser({id:this.MyData.id,fullName:this.fullName.value,phoneNumber:this.phoneNumber.value}).subscribe({
        next:(res)=>{
          this.GetProfile();
          this._toast.showToaster("Data Updated");
        }
      });
    }
    else{
      this.FormData.markAllAsDirty();
    }
  }

changePassword(){
  if(this.FormChangePassword.valid){
    this._authservice.ChangePasswordService({currentPassword:this.FormChangePassword.value.currentPassword,newPassword:this.FormChangePassword.value.newPassword}).subscribe({
      next:(res)=>{
        this.GetProfile();
        this._toast.showToaster("password changed");
      },
      error:(err)=>{
        this._toast.showToaster("Invalid current Password");
      }
    });
  }
  else{
    this.FormChangePassword.markAllAsDirty();
  }
}


ConfirmPasswordMatch (Password:AbstractControl):ValidatorFn{
    return (ConfirmPassword:AbstractControl)=>{
      if(Password.value !== ConfirmPassword.value){
        return {notSame:true};
      }
      return null;
    }
}


displayChangePassword(){
  document.getElementsByClassName("formchange")[0].classList.toggle("hide");
  document.getElementsByClassName("btnchangex")[0].classList.toggle("hide");
}

}
