import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../Models/response.model';
import { Car } from '../Models/car.model';
import { UserService } from './UserService';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  carsUrl = 'http://localhost:5080/api/Cars';
  constructor(private _http: HttpClient, private _userService: UserService) { }

  getUserCars(userName: string): Observable<ApiResponse> {
    return this._http.get<ApiResponse>(`${this.carsUrl}/GetCarsByUserName?userName=${userName}`);
  }

  postCar(car: Car): Observable<ApiResponse> {
    car.ownerUserName = this._userService.getLoggedUser().userName;
    return this._http.post<ApiResponse>(this.carsUrl, car);
  }

  deleteCar(carPlate: string): Observable<ApiResponse> {
    return this._http.delete<ApiResponse>(`${this.carsUrl}?carPlate=${carPlate}`);
  }
}