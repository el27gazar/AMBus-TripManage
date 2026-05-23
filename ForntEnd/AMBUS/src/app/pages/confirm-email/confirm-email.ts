import {  Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../Core/Services/auth-service';

@Component({
  selector: 'app-confirm-email',
  imports: [ReactiveFormsModule],
  templateUrl: './confirm-email.html',
  styleUrl: './confirm-email.css',
})
export class ConfirmEmail {
Code!:FormControl;
validationForm!:FormGroup;
Email:string='';
count:number=40;

constructor(private _route:ActivatedRoute, private _authService:AuthService,private _router:Router){
  this.initFormconterol();
  this.initFormGroup();
  this.Email =this._route.snapshot.queryParams['Email'];
}

initFormconterol(){
  this.Code = new FormControl('',[Validators.required]);
}

initFormGroup(){
  this.validationForm = new FormGroup({
    Code: this.Code
  });
}

VerifyEmail(){
  this._authService.ConfirmEmailService({Email:this.Email,Token:this.validationForm.value.Code}).subscribe({
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

Resend(){
   this._authService.ResendConfirmationService({Email:this.Email}).subscribe({
     next:(res)=>{},
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
