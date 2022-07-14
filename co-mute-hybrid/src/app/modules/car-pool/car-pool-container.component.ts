import { ICarPoolDto } from './../../services/car-pool/abstractions/car-pool-dto.interface';
import { CarPoolService } from './../../services/car-pool/car-pool.service';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';

@Component({
  selector: 'app-car-pool-container',
  templateUrl: './car-pool-container.component.html',
  styleUrls: ['./car-pool-container.component.scss']
})
export class CarPoolContainerComponent implements OnInit {


  public carPools : ICarPoolDto[] = [];
  public filter : string = "";

  constructor(
    private _carPoolManager : CarPoolService
  ) { }

  private async getCarPoolList() {
    try {
      const res = await this._carPoolManager.getCarPoolList_async();

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

  ngOnInit(): void {
    this.getCarPoolList()
  }

}
