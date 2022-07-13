import { APP_CONFIG } from './../../abstractions/tokens/app-config.token';
import { environment } from './../../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig } from 'src/app/abstractions/interfaces/app-config.interface';



@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private get userApiUrl() {
    return this.config.api.url + '/user';
  }

  constructor(
    private _http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }


}
