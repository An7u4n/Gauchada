import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../Models/response.model';

@Injectable({
  providedIn: 'root'
})
export class TripService {
  tripsUrl = 'http://localhost:5080/api/Trips';
  constructor(private _http: HttpClient) { }

  tripSearch(origin: string, destination: string): Observable<ApiResponse> {
    return this._http.get<ApiResponse>(`${this.tripsUrl}?origin=${origin}&destination=${destination}`);
  }

  postTrip(origin: string, destination: string, date: string, user: string, plate: string): Observable<ApiResponse> {
    return this._http.post<any>(this.tripsUrl, {origin: origin, destination: destination, startDate: date, driverUserName: user, carPlate: plate});
  }
}
