import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class PassengerService {
  passengerUrl = 'http://localhost:5080/api/Passengers';
  constructor(private _http: HttpClient) { }

  getLoggedPassenger(): User | undefined {
    let passenger = JSON.parse(localStorage.getItem('passenger') ?? '');
    return passenger;
  }

  postPassenger(user: User, photo: any): Observable<any> {
    const formData = new FormData();
    formData.append('UserName', user.userName);
    formData.append('Name', user.name);
    formData.append('LastName', user.lastName);
    formData.append('Birth', user.birth);
    formData.append('Email', user.email);
    formData.append('PhoneNumber', user.phoneNumber);
    formData.append('Photo', photo);
    return this._http.post<any>(`${this.passengerUrl}`, formData);
  }
}