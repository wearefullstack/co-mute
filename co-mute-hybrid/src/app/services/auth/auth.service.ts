import { APP_CONFIG } from './../../abstractions/tokens/app-config.token';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig } from 'src/app/abstractions/interfaces/app-config.interface';
import { IApiResult } from 'src/app/abstractions/interfaces/api-result.interface';
import { Router } from '@angular/router';

export interface ILoginDto {
  email: string;
  password: string;
}

export interface IUser {
  userId: string;
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
    private _router: Router,
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public async login_async(loginData: ILoginDto) {
    const url = this.userApiUrl + '/login';
    return await this._http.post<IApiResult<IUser>>(url, loginData).toPromise();
  }

  public logOut(): void {
    sessionStorage.removeItem(this.USER_SESSION_KEY);
    this.goToLogin();
  }

  public setUserSession(user: IUser) {
    sessionStorage.setItem(this.USER_SESSION_KEY, JSON.stringify(user));
  }

  public getUserSession(): IUser | null {
    try {
      return JSON.parse(sessionStorage.getItem(this.USER_SESSION_KEY) as string);
    } catch (error) {
      return null;
    }
  }

  public isAuthenticated(): boolean {
    return this.getUserSession() ? true : false;
  }

  public goToLogin() {
    this._router.navigate(['login']);
  }

}
