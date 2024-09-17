import { Component, Input, OnInit } from '@angular/core';
import { Trip } from '../../Models/trip.model';
import { TripService } from '../../Services/TripService';
import { User } from '../../Models/user.model';

@Component({
  selector: 'app-trip-detail',
  templateUrl: './trip-detail.component.html',
  styleUrl: './trip-detail.component.css'
})
export class TripDetailComponent implements OnInit{
  trip!: Trip;
  passengers: User[] = [];

  constructor(private _tripService: TripService) { }

  ngOnInit(): void {
    this.trip = this._tripService.getSavedTrip();
    this._tripService.getTripPassengers(this.trip.tripId).subscribe(responseMessage => {
      this.passengers = responseMessage.data;
      this.passengers.forEach(passenger => {
        passenger.photoSrc = 'http://localhost:5080/images/passenger/' + passenger.photoSrc;

      });
    })
  }
}
