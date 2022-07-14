import { ICarPoolDto } from './abstractions/car-pool-dto.interface';
import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig } from 'src/app/abstractions/interfaces/app-config.interface';
import { APP_CONFIG } from 'src/app/abstractions/tokens/app-config.token';
import { IApiResult } from 'src/app/abstractions/interfaces/api-result.interface';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class CarPoolService {

  private get carPoolApiUrl() {
    return this.config.api.url + '/car-pool';
  }

  constructor(
    private _http: HttpClient,
    private _authManager : AuthService,
    @Inject(APP_CONFIG) private config: AppConfig
  ) { }

  public async addCarPool_async(data : Partial<ICarPoolDto>){
    const url = this.carPoolApiUrl
    const body : ICarPoolDto = {
      ...data as ICarPoolDto,
      userId : this._authManager.getUserSession()?.userId as string
    }
    return await this._http.post<IApiResult<ICarPoolDto>>(url, body).toPromise()
  }

  public async getCarPoolList_async() {
    const url = this.carPoolApiUrl;
    return await this._http.get<ICarPoolDto[]>(url).toPromise()
  }

}
