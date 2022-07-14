import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../services/auth/auth.service";


@Injectable({
    providedIn: 'root'
  })
  export class AuthGuard {
  
    constructor(private _authManager: AuthService, public router: Router) { }
  
    async canActivate(): Promise<boolean> {
      if (this._authManager.isAuthenticated()) {
        return true;
      }
      this._authManager.goToLogin();
      return false;
    }
  }