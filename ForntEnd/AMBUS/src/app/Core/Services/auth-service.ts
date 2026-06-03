import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseUrl } from '../baseUrl';
import { IRegister } from '../interfaces/iregister';
import { Observable } from 'rxjs';
import { ILogin } from '../interfaces/ilogin';
import { IresetPassword } from '../interfaces/ireset-password';
import { IChangePassword } from '../interfaces/ichange-password';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private _httpClient:HttpClient) {}

  RegisterService(registerData:IRegister):Observable<any>{
     return this._httpClient.post(BaseUrl+"/Auth/register",registerData);
  }


  LoginService(Data:ILogin):Observable<any>{
    return this._httpClient.post(BaseUrl+"/Auth/login",Data,);
  }

  ForgetPasswordService(Data:{Email:string}):Observable<any>{
     return this._httpClient.post(BaseUrl+"/Auth/forgot-password",Data);
  }

  ResetPasswordService(Data:IresetPassword):Observable<any>{
    return this._httpClient.post(BaseUrl+"/Auth/reset-password",Data);
  }

  ChangePasswordService(Data:IChangePassword):Observable<any>{
    return this._httpClient.post(BaseUrl+"/Auth/change-password",Data);
  }

    ConfirmEmailService(Data:{Email:string,Token:string}):Observable<any>{
    console.log(Data);
    return this._httpClient.post(BaseUrl+"/Auth/confirm-email"+"?Email="+Data.Email+"&Token="+Data.Token,"");
  }

  ResendConfirmationService(Data:{Email:string}):Observable<any>{
    return this._httpClient.post(BaseUrl+"/Auth/resend-confirmation",Data);

  }


}
