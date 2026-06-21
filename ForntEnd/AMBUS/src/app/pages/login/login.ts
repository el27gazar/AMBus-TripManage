import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from "@angular/router";
import { AuthService } from '../../Core/Services/auth-service';
import { ILogin } from '../../Core/interfaces/ilogin';

@Component({
  selector: 'app-login',
  imports: [RouterLink, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  Email!:FormControl ;
  Password!:FormControl ;

  showPassword:boolean = false;
  LoginForm!:FormGroup;


  constructor(private _router:Router , private _authService:AuthService){
    this.initFormControl();
    this.initFormGroup();
  }

  initFormControl(){
    this.Email = new FormControl('',[Validators.required]);
    this.Password = new FormControl('',[Validators.required]);
  }

  initFormGroup(){
    this.LoginForm = new FormGroup({
      Email: this.Email,
      Password: this.Password
    });
  }

  Login(){
      if(this.LoginForm.valid){
        this.LoginService(this.LoginForm.value);
      }else{
        this.LoginForm.markAllAsTouched();
        this.LoginForm.markAllAsDirty();
      }
  }

    LoginService(Data:ILogin):void{
      this._authService.LoginService(Data).subscribe({
        next:(res)=>{
          var role = res.role;
          if(role === 'Admin'){
            this._router.navigateByUrl('/admin');
          }else if(role === 'Driver'){
            this._router.navigate(['driver']);
          }else{
            this._router.navigate(['user/BookTrip']);
          }
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

      ChangePassword(){
    this.showPassword = !this.showPassword;
  }

}
