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

  getUserTrips(username: string) : Observable<ApiResponse> {
    return this._http.get<ApiResponse>(`${this.tripsUrl}/GetUserTrips?userName=${username}`);
  }

  getTripPassengers(tripId: number) : Observable<ApiResponse> {
    return this._http.get<ApiResponse>(`${this.tripsUrl}/GetTripPassengers?tripId=${tripId}`);
  }

  addPassengerToATrip(tripId: number, userName: string) : Observable<ApiResponse> {
    return this._http.post<ApiResponse>(`${this.tripsUrl}/AddPassengerToATrip?tripId=${tripId}&passengerUserName=${userName}`, null);
  }

  tripSearch(origin: string, destination: string, date: any): Observable<ApiResponse> {
    return this._http.get<ApiResponse>(`${this.tripsUrl}/ExactDate?origin=${origin}&destination=${destination}&date=${date}`);
  }

  postTrip(origin: string, destination: string, date: string, user: string, plate: string): Observable<ApiResponse> {
    return this._http.post<any>(this.tripsUrl, {origin: origin, destination: destination, startDate: date, driverUserName: user, carPlate: plate});
  }
}
