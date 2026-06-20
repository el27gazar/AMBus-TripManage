import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../Core/Services/auth-service';
import {  Router } from '@angular/router';

@Component({
  selector: 'app-forget-password',
  imports: [ReactiveFormsModule],
  templateUrl: './forget-password.html',
  styleUrl: './forget-password.css',
})
export class ForgetPassword {
 Email!:FormControl ;
 ForgetForm! : FormGroup ;



  constructor(private _authService:AuthService, private _router :Router){
    this.intiFormContol();
    this.initFormGroup();
  }


  intiFormContol(){
    this.Email = new FormControl('',[Validators.required,Validators.email]);
  }

  initFormGroup(){
    this.ForgetForm = new FormGroup({
      Email : this.Email
    });
  }
 Send(){
   if(this.ForgetForm.valid)
   {
     this.ForgetService(this.ForgetForm.value);
   }
   else{
    this.ForgetForm.markAllAsDirty();
   }
 }


  ForgetService(Data:{Email:string}){
    this._authService.ForgetPasswordService(Data).subscribe({
      next:(res)=>{
        this._router.navigate(['/ResetPassword'],{queryParams:{Email:Data.Email}});
      },
      error:(err)=>{
         document.getElementsByClassName("toast-message")[0].innerHTML = err.error;
            document.getElementById("toastError")?.classList.add("show");
            setTimeout(() => {
            document.getElementById("toastError")?.classList.remove("show");
            },4000);
      }
    });

  }


}
