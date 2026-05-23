import { Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { Router, RouterLink } from "@angular/router";
import { register } from 'module';
import { AuthService } from '../../Core/Services/auth-service';
import { IRegister } from '../../Core/interfaces/iregister';

@Component({
  selector: 'app-register',
  imports: [RouterLink,ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  showPassword:boolean = false;
  FullName!:FormControl;
  EmailAddress!:FormControl;
  Password!:FormControl;
  PhoneNumber!:FormControl;
  ConfirmPassword!:FormControl;
  RegisterForm! :FormGroup;


  constructor(private authService:AuthService, private _router:Router){
    this.initFormControl();
    this.initFormGroup();
  }


  initFormControl(){
    this.FullName = new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)]);
    this.EmailAddress = new FormControl('',[Validators.required,Validators.email]);
    this.PhoneNumber = new FormControl('',[Validators.required,Validators.pattern('[0-9]{11}')]);
    this.Password = new FormControl('',[Validators.required,Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]);
    this.ConfirmPassword = new FormControl('',[Validators.required,this.ConfirmPasswordMatch(this.Password)]);
  }

  initFormGroup(){
    this.RegisterForm = new FormGroup({
      FullName: this.FullName,
      Email: this.EmailAddress,
      Password: this.Password,
      PhoneNumber: this.PhoneNumber,
      ConfirmPassword: this.ConfirmPassword
    });
  }


  Submit():void{
    if(this.RegisterForm.valid)
    {

      this.Register(this.RegisterForm.value);
    }
    else{
        this.RegisterForm.markAllAsTouched();
       this.RegisterForm.markAllAsDirty();
    }
  }

  Register(Date:IRegister):void{
    this.authService.RegisterService(Date).subscribe({
      next:(res)=>{
          this._router.navigate(['/ConfirmEmail'],{queryParams:{Email:Date.Email}});
      }
      ,
      error:(err)=>{
        document.getElementsByClassName("toast-message")[0].innerHTML = err.error.errors[0];
        document.getElementById("toastError")?.classList.add("show");
        setTimeout(() => {
        document.getElementById("toastError")?.classList.remove("show");
        },4000);
      }
    }
)
  }


  ChangePassword(){
    this.showPassword = !this.showPassword;
  }


  ConfirmPasswordMatch (Password:AbstractControl):ValidatorFn{
    return (ConfirmPassword:AbstractControl)=>{
      if(Password.value !== ConfirmPassword.value){
        return {notSame:true};
      }
      return null;
    }

  }

}
