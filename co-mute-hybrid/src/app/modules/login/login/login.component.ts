import { AuthService, ILoginDto } from './../../../services/auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {


  public loading: boolean = false;
  public loginError: string | undefined = undefined;

  public loginForm = this._fb.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  })

  public getControl(control: string) {
    return this.loginForm.get(control);
  }


  constructor(
    private _fb: FormBuilder,
    private _authManager: AuthService
  ) { }

  public async login_async() {
    this.loginError = undefined;
    this.loading = true;
    try {
      const res = await this._authManager.login_async(this.loginForm.value as ILoginDto);
      if(!res?.success) {
        this.loginError = res?.error as string;
        return;
      }
      this._authManager.setUserSession(res.result);
    } catch (error) {
      console.error(error);
    }
    finally {
      this.loading = false;
    }

  }

  ngOnInit(): void {
  }



}
