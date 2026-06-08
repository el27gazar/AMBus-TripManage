import { ChangeDetectorRef, Component } from '@angular/core';
import { Route } from '../../Core/Services/route';

@Component({
  selector: 'app-select-route',
  imports: [],
  templateUrl: './select-route.html',
  styleUrl: './select-route.css',
})
export class SelectRoute {

  AllRoutes:any = [];
  constructor(private _route:Route,private _cd:ChangeDetectorRef) {}

  ngOnInit() {
    this._route.GetAllRoutes().subscribe({
      next:(res)=>{
        this.AllRoutes = [...res];
        this._cd.markForCheck();
      }
    });
  }

}
