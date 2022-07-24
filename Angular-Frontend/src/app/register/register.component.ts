import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiService } from '../api.service';
import { User } from '../classes/user.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  userData: User;
  users;
  validationForm: FormGroup;

  constructor(public service: ApiService) {}

  ngOnInit(): void {

    this.validationForm = new FormGroup({
      'Name' : new FormControl('', [Validators.required, Validators.maxLength(50)]),
      'Surname' : new FormControl('', [Validators.required, Validators.maxLength(50)]),
      'Phone' : new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]),
      'Email' : new FormControl('', [Validators.required, Validators.maxLength(50), Validators.email]),
      'Password' : new FormControl('', [Validators.required, Validators.maxLength(50)]),
    });
    this.refreshList();
    this.resetForm()
  }
  
  public hasError = (controlName: string, errorName: string) =>{
    return this.validationForm.controls[controlName].hasError(errorName);
  }
  resetForm() {
    this.userData = {
      User_ID: 0,
      Name: '',
      Surname: '',
      Phone: '',
      Email: '',
      Password: '',
    };
  }

  refreshList() {
    this.service
      .userList()
      .then((res) => (this.users = res as User[]) )
      console.log(this.users);
  }

  submit() {
    if (this.userData.User_ID === 0) {
      this.service.registerUser(this.userData).subscribe(
        (res) => {
          this.resetForm();
          this.refreshList();
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
}
