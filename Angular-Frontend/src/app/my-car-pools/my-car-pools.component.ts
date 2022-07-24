import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ApiService } from '../api.service';
import { CarPools } from '../classes/car-pools.model';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-my-car-pools',
  templateUrl: './my-car-pools.component.html',
  styleUrls: ['./my-car-pools.component.css'],
})
export class MyCarPoolsComponent implements OnInit {
  public displayedColumns = [
    'Origin',
    'Destination',
    'Departure Time',
    'Arrival Time',
    'Days',
    'Available Seats',
    'Notes',
  ];
  public joinedColumns = [
    'Origin',
    'Destination',
    'Departure Time',
    'Arrival Time',
    'Days',
    'Available Seats',
    'Owner/Leader',
    'Notes',
    'Leave'
  ];
  public dataSource = new MatTableDataSource<CarPools>();
  public joinedOpps = new MatTableDataSource<CarPools>();
  @ViewChild(MatSort) sort: MatSort;

  constructor(public service: ApiService) {}
  validationForm: FormGroup;
  carpoolData: CarPools;
  carpools: CarPools[]

  ngOnInit(): void { 
    this.validationForm = new FormGroup({
      Origin: new FormControl('', [
        Validators.required,
        Validators.maxLength(50),
      ]),
      Destination: new FormControl('', [
        Validators.required,
        Validators.maxLength(50),
      ]),
      Departure_Time: new FormControl('', Validators.required),
      Expected_Arrival_Time: new FormControl('', Validators.required),
      Days_Available: new FormControl('', Validators.required),
      Available_Seats: new FormControl('', [
        Validators.required,
        Validators.min(1),
      ]),
      Notes: new FormControl('', Validators.maxLength(250)),
    });
    this.resetForm();
    this.refreshList();
    this.getJoinedCarpools();
  }
  
  public doFilter = (value: string) => {
    this.dataSource.filter = value.trim().toLocaleLowerCase();
  };
  public filter = (value: string) => {
    this.joinedOpps.filter = value.trim().toLocaleLowerCase();
  };
  public hasError = (controlName: string, errorName: string) => {
    return this.validationForm.controls[controlName].hasError(errorName);
  };

  resetForm() {
    this.carpoolData = {
      Car_Pool_ID: 0,
      Departure_Time: new Date().toLocaleTimeString(),
      Expected_Arrival_Time: new Date().toLocaleTimeString(),
      Origin: '',
      Days_Available: '',
      Destination: '',
      Available_Seats: 0,
      User_ID: localStorage.User_ID,
      Notes: '',
    };
  }
  refreshList() {
    this.service
      .getAllCarPools()
      .then((res) => (this.dataSource.data = res as CarPools[]));
  }
  getJoinedCarpools(){
    this.service
    .getJoinedCarPools()
    .then((res) => {(this.joinedOpps.data = res as CarPools[]), console.log(this.carpools)});
  }
  leaveCarPool(){
    this.service.leaveCarPool(localStorage.Joined_Car_Pool_ID).subscribe((res) => {
      Swal.fire(
        'Deleted!',
        'The record has been deleted.',
        'success'
      )},   
      (error) => {

        console.log(error)
      });
    
    
      window.location.reload()
    
  }
  getCarPoolID(id){
    localStorage.Joined_Car_Pool_ID = id;    
  }

  submit() {
    // if (this.carpoolData.Car_Pool_ID === 0) {
    this.service.registerCarPool(this.carpoolData).subscribe((res) => {
      this.resetForm();
      this.refreshList();
      this.success();
    });
    // }
  }

  success(){  
    Swal.fire(
      'Success!',
      'Added Successfully!',
      'success'
    )   
  }

  leave(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this record?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Delete'
    }).then((result) => {
      if (result.isConfirmed) {
        this.leaveCarPool();
      }
    })
        
      
      
    
   
  
}
}
