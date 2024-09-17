import { Component, Input, OnInit } from '@angular/core';
import { Trip } from '../../Models/trip.model';
import { TripService } from '../../Services/TripService';
import { User } from '../../Models/user.model';
import { DriverService } from '../../Services/DriverService';

@Component({
  selector: 'app-trip-detail',
  templateUrl: './trip-detail.component.html',
  styleUrl: './trip-detail.component.css'
})
export class TripDetailComponent implements OnInit{
  trip!: Trip;
  passengers: User[] = [];
  driver!: User;

  constructor(private _tripService: TripService, private _driverService: DriverService) { }

  ngOnInit(): void {
    this.trip = this._tripService.getSavedTrip();
    this._tripService.getTripPassengers(this.trip.tripId).subscribe(responseMessage => {
      this.passengers = responseMessage.data;
      this.passengers.forEach(passenger => {
        passenger.photoSrc = 'http://localhost:5080/images/passenger/' + passenger.photoSrc;
      });
    });
    this._driverService.getDriver(this.trip.driverUserName).subscribe(responseMessage => {
      this.driver = responseMessage.data;
      this.driver.photoSrc = 'http://localhost:5080/images/driver/' + this.driver.photoSrc;
    });
  }
}
