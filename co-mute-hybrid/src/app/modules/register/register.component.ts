import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ICreateUserDto, UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public loading: boolean = false;

  public registerForm = this._fb.group({
    name: ['', Validators.required],
    surname: ['', Validators.required],
    email: ['', Validators.required],
    phone: ['',],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
  })

  constructor(
    private _fb: FormBuilder,
    private _userManager: UserService
  ) { }

  ngOnInit(): void {
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
