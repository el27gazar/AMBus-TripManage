import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-manage',
  imports: [ReactiveFormsModule],
  templateUrl: './user-manage.html',
  styleUrl: './user-manage.css',
})
export class UserManage {
 SerachInput!:FormControl;
 SelectValue!:FormControl;

 SearchForm!:FormGroup;


  constructor() {
    this.initFormControl();
    this.initFormGroup();
  }

  initFormControl(){
    this.SerachInput = new FormControl('');
    this.SelectValue = new FormControl('');
  }

  initFormGroup(){
    this.SearchForm = new FormGroup({
      SerachInput: this.SerachInput,
      SelectValue: this.SelectValue
    });
  }

  submit(){
    console.log(this.SearchForm.value);
  }


}
