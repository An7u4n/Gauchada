import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../Models/response.model';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  carsUrl = 'http://localhost:5080/api/Cars';
  constructor(private _http: HttpClient) { }

  getUserCars(userName: string): Observable<ApiResponse> {
    return this._http.get<ApiResponse>(`${this.carsUrl}/GetCarsByUserName?userName=${userName}`);
  }
}