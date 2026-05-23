import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AuthService } from '../../Core/Services/auth-service';
import {  ActivatedRoute, Router } from '@angular/router';
import { IresetPassword } from '../../Core/interfaces/ireset-password';

@Component({
  selector: 'app-reset-password',
  imports: [ReactiveFormsModule],
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.css',
})
export class ResetPassword {
  Token!:FormControl;
  NewPassword!:FormControl;
  ConfirmPassword!:FormControl;
  Email!:FormControl;
   ResetForm! :FormGroup;

   constructor(private _authService:AuthService, private _router:Router, private _route:ActivatedRoute){
     this.intiFormControl();
     this.intiFormGroup();
   }



 intiFormControl(){
   this.Token = new FormControl('',[Validators.required]);
   this.NewPassword = new FormControl('',[Validators.required,Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]);
   this.ConfirmPassword = new FormControl('',[Validators.required,this.ConfirmPasswordMatch(this.NewPassword)]);
   this.Email = new FormControl(this._route.snapshot.queryParams['Email']);
 }

 intiFormGroup(){
   this.ResetForm = new FormGroup({
     Token: this.Token,
     NewPassword: this.NewPassword,
     ConfirmPassword: this.ConfirmPassword,
     Email: this.Email
   });
 }



  ConfirmPasswordMatch (Password:AbstractControl):ValidatorFn{
    return (ConfirmPassword:AbstractControl)=>{
      if(Password.value !== ConfirmPassword.value){
        return {notSame:true};
      }
      return null;
    }

  }


   reset(){
    if(this.ResetForm.valid)
    {
      this.ResetService(this.ResetForm.value);
    }
    else{
      this.ResetForm.markAllAsDirty();
    }
  }

  ResetService(Data:IresetPassword){
  this._authService.ResetPasswordService(Data).subscribe({
    next:(res)=>{
      this._router.navigate(['/login']);
    },
    error:(err)=>{
            document.getElementsByClassName("toast-message")[0].innerHTML = err.error.errors[0];
            document.getElementById("toastError")?.classList.add("show");
            setTimeout(() => {
            document.getElementById("toastError")?.classList.remove("show");
            },4000);
    }
  });

  }


}
