import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  driverUrl = 'http://localhost:5080/api/Drivers';
  constructor(private _http: HttpClient) { }

  postDriver(user: User, photo: any): Observable<any> {
    const formData = new FormData();
    formData.append('UserName', user.userName);
    formData.append('Name', user.name);
    formData.append('LastName', user.lastName);
    formData.append('Birth', user.birth);
    formData.append('Email', user.email);
    formData.append('PhoneNumber', user.phoneNumber);
    formData.append('Photo', photo);
    return this._http.post<any>(`${this.driverUrl}`, formData);
  }
}
