import { ActivatedRoute } from '@angular/router';
import { ChangeDetectorRef, Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { User } from '../../Core/Services/user';
import { IUser } from '../../Core/interfaces/iuser';
import { GlobalService } from '../../Core/Services/global-service';
import { CommonModule } from '@angular/common';
import { DriverService } from '../../Core/Services/driver-service';

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

 DriverForm!:FormGroup;
 userId!:FormControl;
 licenseNumber!:FormControl;
 licenseExpiry!:FormControl;
 emergencyContact!:FormControl;
Password!:FormControl;
licenseNumberA!:FormControl;
licenseExpiryA!:FormControl;
emergencyContactA!:FormControl;
Email!:FormControl;

AddForm!:FormGroup;
idDriver:string ="";
 temp:boolean = false;
 driver:any;
 AllUsers:any[] = [];

  constructor(private _userservice:User
    ,private _cd:ChangeDetectorRef
   , private _toast:GlobalService,
  private _drvier:DriverService) {
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
    this.Email = new FormControl('');
    this.fullName = new FormControl('',[Validators.required,Validators.minLength(3),Validators.maxLength(50)]);
    this.phoneNumber = new FormControl('',[Validators.required,Validators.pattern('[0-9]{11}')]);
   this.Password = new FormControl('',[Validators.required,Validators.minLength(8)]);
    this.userId = new FormControl('');
    this.licenseNumber = new FormControl('',[Validators.required]);
    this.licenseExpiry = new FormControl('',[Validators.required]);
    this.emergencyContact = new FormControl('',[Validators.required,Validators.pattern('[0-9]{11}')]);

     this.licenseNumberA = new FormControl('',[Validators.required]);
    this.licenseExpiryA = new FormControl('',[Validators.required]);
    this.emergencyContactA = new FormControl('');
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
      role: this.role,
    });

    this.DriverForm = new FormGroup({
      userId: this.userId,
      licenseNumber: this.licenseNumber,
      licenseExpiry: this.licenseExpiry,
      emergencyContact: this.emergencyContact
    })

    this.AddForm = new FormGroup({
      fullName: this.fullName,
      email: this.Email,
      phoneNumber: this.phoneNumber,
      licenseNumber: this.licenseNumberA,
      licenseExpiry: this.licenseExpiryA,
      emergencyContact: this.emergencyContactA,
      password: this.Password,
    })
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
         this.Email.setValue(res.email);
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

  Ban(item:any,temp:boolean){
     if(temp){
       this.Delete(item);
     }else{
       this.Activated(item.id);
     }
  }


  Delete(item:any){
    if(item.role =="Driver"){
     this._drvier.Delete(item.id).subscribe({
      next:(res)=>{
        this.submit();
      },
      error:(err)=>{
               this._toast.showToaster(err.error.errors[0]);
      }
     })
    }
    this._userservice.DeleteUser(item.id).subscribe({
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
     if(this.UpdateForm.value.fullName && this.UpdateForm.value.phoneNumber && this.UpdateForm.value.role){
      this.UpdateRole(this.UpdateForm.value.id,this.UpdateForm.value.role);
       setTimeout(() => {
        this._userservice.UpdateUser({id:this.UpdateForm.value.id,fullName:this.fullName.value,phoneNumber:this.phoneNumber.value}).subscribe({
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


  getDriver(id:string){
     this._drvier.GetAllDrivers().subscribe({
        next:(res)=>{
            this.driver = res.find((item:any)=>item.userId == id);
            this.temp = true;
            this.idDriver = this.driver.id;
            this.DriverForm.get("licenseNumber")?.setValue(this.driver.licenseNumber);
            this.DriverForm.get("licenseExpiry")?.setValue(this.driver.licenseExpiry);
            this.DriverForm.get("emergencyContact")?.setValue(this.driver.emergencyContact);
        }
     })
    this.DriverForm.get("userId")?.setValue(id);
    document.getElementsByClassName("modal-driver")[0].classList.toggle("hide");
    document.getElementsByClassName("overlay")[0].classList.toggle("hide");
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

AddDriver()
{
  if(this.AddForm.valid)
  {
    this._drvier.Create(this.AddForm.value).subscribe({
      next:(res)=>{
        this._toast.showToaster("Driver Added Successfully");
        this.OpenModal();
        this.submit();
        this._cd.detectChanges();
      },
      error:(err)=>{
               this.OpenModal();
          this._toast.showToaster(err.error.errors[0]);
      }
    })
  }


}

UpdateDriver(){
  if(this.DriverForm.valid){
    this._drvier.Update(this.idDriver,this.DriverForm.value).subscribe({
      next:(res)=>{
        this._toast.showToaster("Driver Updated Successfully");
        this.submit();
        this._cd.detectChanges();
      },
      error:(err)=>{
        console.log(err);
          this._toast.showToaster(err.error.errors[0]);
      }
    })
    document.getElementsByClassName("overlay")[0].classList.toggle("hide");
    document.getElementsByClassName("modal-driver")[0].classList.toggle("hide");
  }
  else{
    this.DriverForm.markAllAsDirty();
  }
}


GetDrivers(){
  this._drvier.GetAllDrivers().subscribe({
    next:(res)=>{
      this.AllUsers = [...res.items];
      this._cd.detectChanges();
    },
    error:(err)=>{
      console.log(err);
        this._toast.showToaster(err.error.errors[0]);
    }
  });
}

closeDriverModal(){
   document.getElementsByClassName("modal-driver")[0].classList.toggle("hide");
    document.getElementsByClassName("overlay")[0].classList.toggle("hide");
}

OpenModal(){
  this.AddForm.reset();
  document.getElementsByClassName("modal-AddDriver")[0].classList.toggle("hide");
    document.getElementsByClassName("overlay")[0].classList.toggle("hide");
}
}
