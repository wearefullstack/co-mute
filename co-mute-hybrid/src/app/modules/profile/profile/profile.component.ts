import { IUser, UserService } from 'src/app/services/user/user.service';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  public user : any;

  constructor(
    private _userManager : UserService
  ) { }

  private async getData() {
    try {
      const res = await this._userManager.getUserData();

      // if(!res?.success){
      //   return;
      // }
      this.user = res;

    } catch (error) {
      console.error(error)
    }
  }

  ngOnInit(): void {
    this.getData();
  }

}
