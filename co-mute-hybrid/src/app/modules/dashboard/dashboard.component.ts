import { UserService } from 'src/app/services/user/user.service';
import { Router } from '@angular/router';
import { AuthService, IUser } from './../../services/auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { ICarPoolDto } from 'src/app/services/car-pool/abstractions/car-pool-dto.interface';
import * as moment from 'moment';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  public user : IUser | null = null;
  public carPools : ICarPoolDto[] = [];
  public filter : string = "";

  constructor(
    private _authManager : AuthService,
    private _router : Router,
    private _userManager : UserService
  ) { }

  goToCreateCarPool(){
    this._router.navigate(['car-pool','create']);
  }

  goToSearchCarPool(){
    this._router.navigate(['car-pool','search']);
  }

  ngOnInit(): void {
    this.user = this._authManager.getUserSession();
    this.getCarPoolList();
  }

  private async getCarPoolList() {
    try {
      const res = await this._userManager.getPoolData();

      // if(!res?.success){
      //   return;
      // }
      this.carPools = res as ICarPoolDto[];

    } catch (error) {
      console.error(error)
    }
  }

  public getDuration(start : Date, end : Date){
    return moment.duration(moment(end).diff(moment(start))).humanize();
  }

}
