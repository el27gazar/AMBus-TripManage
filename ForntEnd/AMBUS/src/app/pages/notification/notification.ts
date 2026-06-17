import { ChangeDetectorRef, Component } from '@angular/core';
import { NotificationService } from '../../Core/Services/notification-service';
import { GlobalService } from '../../Core/Services/global-service';

@Component({
  selector: 'app-notification',
  imports: [],
  templateUrl: './notification.html',
  styleUrl: './notification.css',
})
export class Notification {
AllNotifications  :any[]=[];
type:string='';
message:string='';
  constructor(private notifiy:NotificationService,
    private _cd:ChangeDetectorRef,
    private _toast:GlobalService
  ) {
    this.GetAll();
  }


  GetAll(){
    this.notifiy.GetAll().subscribe({
      next:(res)=>{
        this.AllNotifications = [...res];
        this._cd.markForCheck();
      }
    })
  }


  open(id:string){
    this.notifiy.MakeRead(id).subscribe({
      next:(res)=>{
         document.getElementById(id)?.classList.toggle('close');
      },
      error:(err)=>{
        console.log(err);
      this._toast.showToaster(err.error.errors[0]);
      }
    })

  }

  delete(id:string){
    this.notifiy.Delete(id).subscribe({
      next:(res)=>{
        this.GetAll();
        this._cd.markForCheck();
      },
      error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
      }
    })
  }

  MakeAllRead(){
    this.notifiy.MakeAllRead().subscribe({
      next:(res)=>{
        this.GetAll();
        this._cd.markForCheck();
      },
      error:(err)=>{
      this._toast.showToaster(err.error.errors[0]);
      }
    })
  }
}
