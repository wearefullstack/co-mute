import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CarPools } from './classes/car-pools.model';
import { JoinedCarPools } from './classes/joined-car-pools.model';
import { User } from './classes/user.model';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    "Access-Control-Allow-Origin": '*'
  })
};

const httpText = {
  headers: new HttpHeaders({
    'Content-Type':  'text/plain',
    "Access-Control-Allow-Origin": '*'
  })
};

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  ApiUrl= 'http://localhost:59598/api'

  constructor(private http: HttpClient) { }

  //User
  registerUser(obj: User) {
    return this.http.post(this.ApiUrl + '/User/registerNewUser', obj);
  }

  userList() {
    return this.http.get(this.ApiUrl + '/User/getUsers').toPromise();
  }

  public Login(user : User) {
    return this.http.post<User>(this.ApiUrl + '/User/userLogin',user, httpOptions);
  }

  //Car Pools
  registerCarPool(obj: CarPools) {
    return this.http.post(this.ApiUrl + '/Car_Pool', obj);
  }

  getAllCarPools() {
    return this.http.get(this.ApiUrl + '/Car_Pool/allCarPools').toPromise();
  }

  //Joined Car pools
  joinCarPool(obj: JoinedCarPools) {
    return this.http.post(this.ApiUrl + '/Joined_Car_Pool', obj);
  }
  getJoinedCarPools() {
    return this.http.get(this.ApiUrl + '/Joined_Car_Pool/myCarPools/' + localStorage.User_ID).toPromise();
  }
  leaveCarPool(id: number) {
    return this.http.delete(this.ApiUrl + '/Joined_Car_Pool/' + id);
  }
}
