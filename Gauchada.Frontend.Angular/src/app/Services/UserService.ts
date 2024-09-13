import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, switchMap } from 'rxjs';
import { ApiResponse } from '../Models/response.model';
import { DriverService } from './DriverService';
import { PassengerService } from './PassengerService';
import { User } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  driverUrl = 'http://localhost:5080/api/Drivers';
  passengersUrl = 'http://localhost:5080/api/Passengers';
  constructor(private _http: HttpClient, private _driverService: DriverService, private _passengerService: PassengerService) { }

  loginDriver(username: string, password: string): Observable<boolean> {
    return this._http.post<ApiResponse>(`http://localhost:5080/api/UserLogin/Driver?username=${username}&password=${password}`, {})
    .pipe(
      map(res => {
        localStorage.setItem('token', res.data);
        return res.data;
      }),
      switchMap(token => {
        return this._http.get<ApiResponse>(`${this.driverUrl}?driverUserName=${username}`);
      }),
      map(res => {
        if (res.success && res.data) {
          localStorage.setItem('driver', JSON.stringify(res.data));
          return true;
        } else {
          return false;
        }
      })
    );
  }

  loginPassenger(username: string, password: string): Observable<boolean> {
    return this._http.get<ApiResponse>(`${this.passengersUrl}?passengerUserName=${username}`).pipe(
      map(res => {
        if (res.success && res.data) {
          localStorage.setItem('passenger', JSON.stringify(res.data));
          return true;
        } else {
          return false;
        }
      })
    );
  }

  isLogged(): boolean {
    if(localStorage.getItem('driver') == null && localStorage.getItem('passenger') == null)
      return false;
    else return true;
  }

  getLoggedUser(): User{
    let driver = this._driverService.getLoggedDriver();
    if(driver){
      localStorage.setItem('userType', 'driver');
      return driver;
    }
    let passenger = this._passengerService.getLoggedPassenger();
    if(passenger){
      localStorage.setItem('userType', 'passenger');
      return passenger;
    }
    throw new Error('No user logged');
  }
  getLoggedUserType(): string{
    let userType = localStorage.getItem('userType');
    if(userType)
      return userType;
    throw new Error('No user logged');
  }



  logout(){
    localStorage.removeItem('driver');
    localStorage.removeItem('passenger');
  }
}