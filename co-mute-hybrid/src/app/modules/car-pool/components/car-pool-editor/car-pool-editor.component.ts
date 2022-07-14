import { AppNavigationService } from './../../../../services/app-navigation/app-navigation.service';
import { ICarPoolDto } from './../../../../services/car-pool/abstractions/car-pool-dto.interface';
import { CarPoolService } from './../../../../services/car-pool/car-pool.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';

interface ICarPoolForm {
  departureDate: { year: number, month: number, day: number };
  departureTime: { hour: number; minute: number };
  expectedArrivalDate: { year: number, month: number, day: number };
  expectedArrivalTime: { hour: number; minute: number };
  origin: string;
  destination: string;
  availableSeats: number
  notes: string;
}

enum EDaysOfWeek {
  Monday = 1,
  Tuesday = 2,
  Wednesday = 3,
  Thursday = 4,
  Friday = 5,
  Saturday = 6,
  Sunday = 7
}

@Component({
  selector: 'app-car-pool-editor',
  templateUrl: './car-pool-editor.component.html',
  styleUrls: ['./car-pool-editor.component.scss']
})
export class CarPoolEditorComponent implements OnInit {

  public readonly PLACES = ['CPT', 'JHB', 'PE'];

  public DAYS_AVAILABLE = Object.values(EDaysOfWeek).filter(value => typeof value === 'number') as number[];

  public loading = false;

  public carPoolForm = this._fb.group({
    departureDate: ['', Validators.required],
    departureTime: ['', Validators.required],
    expectedArrivalDate: ['', Validators.required],
    expectedArrivalTime: ['', Validators.required],
    origin: ['', Validators.required],
    destination: ['', Validators.required],
    // daysAvailable: ['', Validators.required], //TODO: Add business logic to handle day availability
    availableSeats: ['', Validators.required],
    notes: ['', Validators.required],
  })

  public getControl(control: string) {
    return this.carPoolForm.get(control);
  }

  constructor(
    private _fb: FormBuilder,
    private _carPoolManager: CarPoolService,
    private _appNavManager : AppNavigationService
  ) { }

  public getDay(day: number) {
    return EDaysOfWeek[day]
  }

  public async postCarPool_async() {
    this.loading = true;
    try {
      const data = this.mapToDto(this.carPoolForm.value as unknown as ICarPoolForm);
      const res = await this._carPoolManager.addCarPool_async(data);

      if(!res?.success){
        alert(res?.error);
      }

      alert("Booking created!");
      this._appNavManager.goToDashboard();

    } catch (error) {
      console.error(error);
    }
    finally {
      this.loading = false;
    }

  }

  private getNormalizedDate(dateObj: { day: number, month: number, year: number }, time: { hour: number; minute: number; }) : Date {
    const date = moment().year(dateObj.year).month(dateObj.month - 1).date(dateObj.day)
    const norm = date.startOf('day').add(time.hour, 'hours').add(time.minute, 'minutes').toDate();
    return norm;
  }

  private mapToDto(input : ICarPoolForm) : Partial<ICarPoolDto> {
    const {  origin, destination, availableSeats, notes } = input;
    return {
      origin,
      destination,
      availableSeats,
      notes,
      depart : this.getNormalizedDate(input.departureDate, input.departureTime),
      arrive : this.getNormalizedDate(input.expectedArrivalDate, input.expectedArrivalTime)
    }
  }


  ngOnInit(): void {
  }

}



