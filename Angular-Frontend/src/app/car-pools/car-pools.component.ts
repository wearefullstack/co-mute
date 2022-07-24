import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import Swal from 'sweetalert2';
import { ApiService } from '../api.service';
import { JoinedCarPools } from '../classes/joined-car-pools.model';
import { CarPools } from '../classes/car-pools.model';

@Component({
  selector: 'app-car-pools',
  templateUrl: './car-pools.component.html',
  styleUrls: ['./car-pools.component.css'],
})
export class CarPoolsComponent implements OnInit {
  public displayedColumns = [
    'Origin',
    'Destination',
    'Departure Time',
    'Arrival Time',
    'Days',
    'Available Seats',
    'Owner/Leader',
    'Notes',
    "Join"
  ];
  public dataSource = new MatTableDataSource<CarPools>();
  @ViewChild(MatSort) sort: MatSort;

  constructor(public service: ApiService) {}
  validationForm: FormGroup;
  joinedData: JoinedCarPools;

  ngOnInit(): void {
    this.validationForm = new FormGroup({
      Name: new FormControl('', [
        Validators.required,
        Validators.maxLength(20),
      ]),
      Description: new FormControl('', [
        Validators.required,
        Validators.maxLength(200),
      ]),
    });
    this.resetForm();
    this.refreshList();
  }
  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  };
  public hasError = (controlName: string, errorName: string) => {
    return this.validationForm.controls[controlName].hasError(errorName);
  };

  resetForm() {
    this.joinedData = {
      Joined_Car_Pool_ID: 0,
      User_ID: localStorage.User_ID,
      Car_Pool_ID: localStorage.Car_Pool_ID,
      Date_Joined: new Date(),
      Departure_Time: localStorage.Departure_Time,
      Expected_Arrival_Time: localStorage.Expected_Arrival_Time

    };
  }
  refreshList() {
    this.service
      .getAllCarPools()
      .then((res) => {(this.dataSource.data = res as CarPools[]), console.log(this.dataSource)});
  }
  getCarPoolID(id, depart, arrive){
    localStorage.Car_Pool_ID = id;
    localStorage.Departure_Time = depart;
    localStorage.Expected_Arrival_Time = arrive;

  }

  joinCarPool() {
    // if (this.carpoolData.Car_Pool_ID === 0) {
    this.service.joinCarPool(this.joinedData).subscribe((res) => {   
      this.resetForm();
      this.refreshList();
      this.success();
    });
    // }
  }

  success(){  
    Swal.fire(
      'Success!',
      'Joined Successfully!',
      'success'
    )   
  }
}
