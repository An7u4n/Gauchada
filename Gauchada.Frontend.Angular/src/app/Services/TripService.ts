import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TripService {
  tripsUrl = 'http://localhost:5080/api/Trips';
  constructor(private _http: HttpClient) { }

  tripSearch(origin: string, destination: string): Observable<any[]> {
    return this._http.get<any[]>(`${this.tripsUrl}?origin=${origin}&destination=${destination}`);
  }
}
