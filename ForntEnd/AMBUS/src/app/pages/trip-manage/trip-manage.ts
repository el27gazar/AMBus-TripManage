import { ChangeDetectorRef, Component } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { TripService } from '../../Core/Services/trip-service';
import { GlobalService } from '../../Core/Services/global-service';
import { Route } from '../../Core/Services/route';
import { DriverService } from '../../Core/Services/driver-service';
import { Bus } from '../../Core/Services/bus';
import { SelectRoute } from "../select-route/select-route";
import { disabled } from '@angular/forms/signals';

@Component({
  selector: 'app-trip-manage',
  imports: [ReactiveFormsModule, SelectRoute],
  templateUrl: './trip-manage.html',
  styleUrl: './trip-manage.css',
})
export class TripManage {

  From!:FormControl;
  To!:FormControl;
  bus!:FormControl;
  driver!:FormControl;
  departureTime!:FormControl;
  arrivalTime!:FormControl;
  basePrice!:FormControl;
  Status!:FormControl;
  availableSeats!:FormControl;
  busType!:FormControl;
  busPlateNumber!:FormControl;

  FromAdd!:FormControl;
  ToAdd!:FormControl;
  busAdd!:FormControl;
  driverAdd!:FormControl;
  departureTimeAdd!:FormControl;
  arrivalTimeAdd!:FormControl;
  basePriceAdd!:FormControl;

  Formgroup!:FormGroup;
  FromDetails!:FormGroup;
  AllTrips:any[]=[];
  AllBuses:any[]=[];
  AllDrivers:any[]=[];
  AllRoutes:any[]=[];
  header:boolean=true;




  initFormControl(){
    this.From = new FormControl({value:'',disabled:true},[Validators.required]);
    this.To = new FormControl({value:'',disabled:true},[Validators.required]);
    this.bus = new FormControl('',[Validators.required]);
    this.driver = new FormControl('',[Validators.required]);
    this.departureTime = new FormControl('',[Validators.required]);
    this.arrivalTime = new FormControl('',[Validators.required]);
    this.basePrice = new FormControl('',[Validators.required]);
    this.Status = new FormControl({value:'',disabled:true},[Validators.required]);
    this.availableSeats = new FormControl({value:'',disabled:true},[Validators.required]);
    this.busType = new FormControl({value:'',disabled:true},[Validators.required]);
    this.busPlateNumber = new FormControl({value:'',disabled:true},[Validators.required]);
    this.FromAdd = new FormControl('',[Validators.required,this.FromValidate(this.ToAdd)]);
    this.ToAdd = new FormControl('',[Validators.required,this.FromValidate(this.FromAdd)]);
    this.busAdd = new FormControl('',[Validators.required]);
    this.driverAdd = new FormControl('',[Validators.required]);
    this.departureTimeAdd = new FormControl('',[Validators.required,this.departureTimeValidate]);
    this.arrivalTimeAdd = new FormControl('',[Validators.required,this.arrivelTimeValidate(this.departureTimeAdd)]);
    this.basePriceAdd = new FormControl('',[Validators.required]);

  }

  initFormGroup(){
    this.Formgroup = new FormGroup({
      fromId : this.FromAdd,
      toId : this.ToAdd,
      busId: this.busAdd,
      driverId : this.driverAdd,
      departureTime : this.departureTimeAdd,
      arrivalTime : this.arrivalTimeAdd,
      basePrice : this.basePriceAdd
    });

    this.FromDetails = new FormGroup({
      id: new FormControl(''),
      From : this.From,
      To : this.To,
      bus : this.bus,
      driver : this.driver,
      departureTime : this.departureTime,
      arrivalTime : this.arrivalTime,
      basePrice : this.basePrice,
      Status : this.Status,
      availableSeats : this.availableSeats,
      busType : this.busType,
      busPlateNumber : this.busPlateNumber
    });
  }

  constructor(private _tripService:TripService,
    private _cd:ChangeDetectorRef,
    private _toast:GlobalService,
    private _route:Route,
    private _driver:DriverService,
    private _bus:Bus
  ) {
    this.initFormControl();
    this.initFormGroup();
  }

  ngOnInit(){
    this.GetAll();
    this.Helper();
  }

  GetAll(){
    this._tripService.GetAll().subscribe({
      next:(res)=>{
        this.AllTrips = [...res.items];
        this._cd.markForCheck();
      },
      error:(err)=>{
        this._toast.showToaster(err.error.errors[0]);
      }
    })

  }

  GetById(id:string){
   this._tripService.GetById(id).subscribe({
    next:(res)=>{
        this.FromDetails.get("id")?.setValue(res.id);
        this.FromDetails.get("From")?.setValue(res.fromId);
        this.FromDetails.get("To")?.setValue(res.toId);
        this.FromDetails.get("bus")?.setValue(res.bus);
        this.FromDetails.get("driver")?.setValue(res.driverId);
        this.FromDetails.get("departureTime")?.setValue(res.departureTime);
        this.FromDetails.get("arrivalTime")?.setValue(res.arrivalTime);
        this.FromDetails.get("basePrice")?.setValue(res.basePrice);
        this.FromDetails.get("Status")?.setValue(res.status);
        this.FromDetails.get("availableSeats")?.setValue(res.availableSeats);
        this.FromDetails.get("busType")?.setValue(res.busType);
        this.FromDetails.get("busPlateNumber")?.setValue(res.busPlate);
        this._toast.closeModal();
        this._cd.markForCheck();
    },
    error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
      this._toast.closeModal();
    }

   })
  }

  Create(){
    console.log(this.Formgroup.value);
    if(this.Formgroup.valid){
    this._tripService.Create(this.Formgroup.value).subscribe({
      next:(res)=>{
        this.GetAll();
        this._cd.markForCheck();
      },
      error:(err)=>{
        console.log(err);
        this._toast.showToaster(err.error.errors[0]);
      }
    })
  }else{
    this.Formgroup.markAllAsDirty();
  }
  this.closeAdd();
}


  Update(){
    this._tripService.Update(this.FromDetails.get("id")?.value,
  {driverId:this.FromDetails.get("driver")?.value,departureTime:this.FromDetails.get("departureTime")?.value,arrivalTime:this.FromDetails.get("arrivalTime")?.value,basePrice:this.FromDetails.get("basePrice")?.value}).subscribe({
      next:(res)=>{
        this.GetAll();
        this._cd.markForCheck();
      },
      error:(err)=>{
        this._toast.showToaster(err.error.errors[0]);
      }
    });
    this.closeEdit();
  }

   Delete(id:string){
    this._tripService.Delete(id).subscribe({
      next:(res)=>{
        this.GetAll();
        this._cd.markForCheck();
      },
      error:(err)=>{
        this._toast.showToaster(err.error.errors[0]);
      }
    })
   }



   Helper(){
     this._bus.Search({type:'',isActive:true}).subscribe({
       next:(res)=>{
         this.AllBuses = [...res];
         this._cd.markForCheck();
       },
       error:(err)=>{
         this._toast.showToaster(err.error.errors[0]);
       }
     });

     this._driver.GetAllDrivers(true).subscribe({
       next:(res)=>{
         this.AllDrivers = [...res];
         this._cd.markForCheck();
       },
       error:(err)=>{
         this._toast.showToaster(err.error.errors[0]);
       }
     });
   }


   FromValidate (From:AbstractControl):ValidatorFn{
    return (To:AbstractControl)=>{
      if(From?.value == To?.value){
        return {notsame:true};
      }
      return null;
    }
  }



   departureTimeValidate(control:AbstractControl):ValidationErrors|null{
    var deteControl = new Date(control.value);
    var datenow = new Date(Date.now());
        if(deteControl < datenow){
           return {notsame:true};
        }
        return null;
   }

   arrivelTimeValidate(arrive:AbstractControl):ValidatorFn{
    return (depart:AbstractControl)=>{
        if(depart.value <= arrive.value){
       return {notsame:true};
     }
     return null;

    }
   }
   closeEdit(){
      document.getElementsByClassName("edit-modal")[0].classList.toggle("hide");
      document.getElementsByClassName("overlay")[0].classList.toggle("hide");
   }

   closeAdd(){
      document.getElementsByClassName("add-modal")[0].classList.toggle("hide");
      document.getElementsByClassName("overlay")[0].classList.toggle("hide");
   }


}
