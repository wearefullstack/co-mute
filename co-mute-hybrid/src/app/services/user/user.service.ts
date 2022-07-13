import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
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
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }


  public async registerUser_async(user: ICreateUserDto): Promise<IUser | undefined> {
    const url = this.userApiUrl
    return await this._http.post<IUser>(url, user).toPromise()
  }
}
