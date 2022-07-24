import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { User } from '../classes/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public email = "";
  public password = "";

  logUsername : string;
  logPassword : string;

  constructor(private service: ApiService, private router:Router
           ) { }

  userObj = new User;
  succLogin = false;
  emptyUSR=false;
  emptyPSR=false;
  invPass = false;
  ee:boolean;
  loggednIn : Object=null;


  ngOnInit() {
  }  

  customerLogin()
  {
   
    this.userObj.Email = this.email;
    this.userObj.Password = this.password;
    console.log(JSON.stringify(this.userObj));
    
    console.log(JSON.stringify(this.userObj))
    this.service.Login(this.userObj).subscribe((response: any)=> {
      if(response.Message){
        this.succLogin=false;
        this.emptyPSR=false;
        this.emptyUSR=false;
        if(this.password=="" && this.email=="")
        {
          this.emptyPSR=true;
          this.emptyUSR=true;
          return;
        }
        if(this.email=="")
        {
          this.emptyUSR=true;
          return;
        }
        if(this.password=="")
        {
          this.emptyPSR=true;
          return;
        }
    else
      console.log("problem")
      }
      else{
        this.loggednIn = response;
        this.router.navigate(['/mycarpools']); // Change this to home once it has been integrated
        localStorage.setItem('loggedIn', 'loggedIn')
      }
      localStorage["User_ID"] = response["User_ID"];
      localStorage["Name"] = response["Name"];
      localStorage["Surname"] = response["Surname"];
      console.log(response);
    })  

  }

}
