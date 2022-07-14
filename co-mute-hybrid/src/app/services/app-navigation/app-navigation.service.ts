import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AppNavigationService {

  constructor(
    private _router : Router
  ) { }

  public goToDashboard() {
    this._router.navigate(['dashboard']);
  }
}
