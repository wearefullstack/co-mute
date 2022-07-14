import { AuthService } from './services/auth/auth.service';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private _authManager : AuthService) {

  }

  isSignedIn(){
    return this._authManager.isAuthenticated();
  }

  title = 'co-mute-hybrid';

  public getDate(): number {
    return new Date().getFullYear();
  }

  public signOut(){
    this._authManager.logOut();
  }
}