import { APP_CONFIG } from './../../abstractions/tokens/app-config.token';
import { environment } from './../../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig } from 'src/app/abstractions/interfaces/app-config.interface';
import { IApiResult } from 'src/app/abstractions/interfaces/api-result.interface';


export interface ILoginDto {
  email: string;
  password: string;
}

export interface IUser {
  name: string;
  surname: string;
  email: string;
  phone: string;
}
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly USER_SESSION_KEY = "user";

  private get userApiUrl() {
    return this.config.api.url + '/auth';
  }

  constructor(
    private _http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public async login_async(loginData: ILoginDto) {
    const url = this.userApiUrl + '/login';
    return await this._http.post<IApiResult<IUser>>(url, loginData).toPromise();
  }

  public setUserSession(user: IUser) {
    sessionStorage.setItem(this.USER_SESSION_KEY, JSON.stringify(user));
  }

  public getUserSession(): IUser {
    return JSON.parse(sessionStorage.getItem(this.USER_SESSION_KEY) as string);
  }


}
