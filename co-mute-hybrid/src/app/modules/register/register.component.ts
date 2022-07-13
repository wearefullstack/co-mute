import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ICreateUserDto, UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public readonly PASSWORD_LENGTH = 8;

  public loading: boolean = false;

  public registerForm = this._fb.group({
    name: ['', Validators.required],
    surname: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['',],
    password: ['', [Validators.required, Validators.minLength(this.PASSWORD_LENGTH)]],
    confirmPassword: ['', Validators.required],
  })

  public getControl(control: string) {
    return this.registerForm.get(control);
  }

  public get emailCtrl() {
    return this.registerForm.get('email');
  }

  constructor(
    private _fb: FormBuilder,
    private _userManager: UserService
  ) { }

  private registerPasswordMatch() {
    const confirmCtrl = this.registerForm.get('confirmPassword');
    confirmCtrl?.valueChanges.subscribe((val) => {
      if (!val) {
        return;
      }

      const passwordCtrl = this.getControl('password');

      if (!passwordCtrl?.value) {
        return
      }

      if (val?.length < passwordCtrl.value.length) {
        return
      }

      if (passwordCtrl.value === confirmCtrl.value) {
        return;
      }

      confirmCtrl.setErrors({
        notMatch: true
      })
    })
  }

  getErr(){
    return JSON.stringify(this.getControl('password')?.errors)

  }

  ngOnInit(): void {
    this.registerPasswordMatch();
  }

  public async registerUser_async() {
    this.loading = true
    try {
      const data = this.registerForm.value
      delete data.confirmPassword;
      await this._userManager.registerUser_async(this.registerForm.value as ICreateUserDto)
    } catch (error) {
      console.error(error);
    }
    finally {
      this.loading = false
    }
  }

}
