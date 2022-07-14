import { AuthService } from './../auth/auth.service';
import { ICarPoolDto } from './../car-pool/abstractions/car-pool-dto.interface';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { IApiResult } from 'src/app/abstractions/interfaces/api-result.interface';
import { AppConfig } from 'src/app/abstractions/interfaces/app-config.interface';
import { APP_CONFIG } from 'src/app/abstractions/tokens/app-config.token';

export interface ICreateUserDto {
  name: string;
  surname: string;
  email: string;
  password: string;
  phone?: string;
}

export interface IUser {
  name: string;
  surname: string;
  email: string;
  password: string;
  phone?: string;
  carPools? : ICarPoolDto[]
}

@Injectable({
  providedIn: 'root'
})
export class UserService {


  private get userApiUrl() {
    return this.config.api.url + '/users';
  }

  constructor(
    private _http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig,
    private _authManager : AuthService 
  ) { }


  public async registerUser_async(user: ICreateUserDto) {
    const url = this.userApiUrl
    return await this._http.post<IApiResult<IUser>>(url, user).toPromise()
  }

  public async getPoolData() {
    const url = this.userApiUrl + `/${this._authManager.getUserSession()?.userId}`
    const res = await this._http.get<IUser>(url).toPromise() 
    return res?.carPools;
  }

  public async getUserData() {
    const url = this.userApiUrl + `/${this._authManager.getUserSession()?.userId}`
    const res = await this._http.get<IUser>(url).toPromise() 
    return res;
  }


}
