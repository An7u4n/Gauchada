import { Component, Input, OnInit } from '@angular/core';
import { DriverService } from '../../Services/DriverService';
import { User } from '../../Models/user.model';
import { TripService } from '../../Services/TripService';
import { UserService } from '../../Services/UserService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-trip-card-popup',
  templateUrl: './trip-card-popup.component.html',
  styleUrl: './trip-card-popup.component.css'
})
export class TripCardPopupComponent implements OnInit {
  constructor(private _tripService: TripService, private _userService: UserService, private router: Router) { }
  @Input() trip: any;
  @Input() driver: User | undefined;
  passengers: User[] = [];

  startDay: string = '';
  startHour: string = '';

  ngOnInit(): void {
    let date = new Date(this.trip.startDate);
    if(date){
      this.startDay = date.getDate() + '/' + (date.getMonth()+1) + '/' + date.getFullYear();
      this.startHour = date.getHours() + ':' + date.getMinutes();
    }
    this._tripService.getTripPassengers(this.trip.tripId).subscribe(responseMessage =>
      {
        this.passengers = responseMessage.data;
        this.passengers.forEach(passenger => {
          passenger.photoSrc = 'http://localhost:5080/images/passenger/' + passenger.photoSrc;
    
        });
      }
      , error => console.error(error));
      
  }
    
  onTripSign(){
    this._tripService.addPassengerToATrip(this.trip.tripId, this._userService.getLoggedUser().userName).subscribe(responseMessage =>
      {
        if(responseMessage.success) {
          alert('You was signed succesfully to the trip');
          this._tripService.setSavedTrip(this.trip);
          this.router.navigate(['/trip-detail']);
        }
        else {
          alert('Couldnt be signed in the trip');
        }
      }
      , error => console.error(error));
  }
}
