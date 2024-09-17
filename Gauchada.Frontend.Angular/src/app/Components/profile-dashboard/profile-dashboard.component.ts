import { Component, OnInit } from '@angular/core';
import { Trip } from '../../Models/trip.model';
import { Car } from '../../Models/car.model';
import { CarService } from '../../Services/CarService';
import { DriverService } from '../../Services/DriverService';
import { User } from '../../Models/user.model';
import { TripService } from '../../Services/TripService';
import { UserService } from '../../Services/UserService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile-dashboard',
  templateUrl: './profile-dashboard.component.html',
  styleUrl: './profile-dashboard.component.css'
})
export class ProfileDashboardComponent implements OnInit {
  trips: Trip[] = [];
  cars: Car[] = [];
  user: User;
  myCars: boolean = false;
  myTrips: boolean = false;
  hasTrips: boolean = false;
  isDriver: boolean = false;

  constructor(private _carService: CarService, private _userService: UserService, private _tripService: TripService, private _driverService: DriverService, private router: Router) {
    this.user = _userService.getLoggedUser();
    let userType = _userService.getLoggedUserType();
    if(userType == 'driver'){
      this.isDriver = true;
    }
  }

  ngOnInit(): void {
    
    if(this.isDriver){
      this._carService.getUserCars(this.user.userName).subscribe(carRet => {
        if(carRet && carRet.data){
          this.cars = carRet.data;
        }
      }, error =>console.error(error));

      this._driverService.getDriverTrips(this.user.userName).subscribe(tripRet => {
        if(tripRet && tripRet.data){
          this.trips = tripRet.data;
          if(this.trips.length > 0) this.hasTrips = true;
        }
      }, error =>console.error(error));

    } else {

      this._tripService.getUserTrips(this.user.userName).subscribe(tripRet => {
        if(tripRet && tripRet.data){
          this.trips = tripRet.data;
          if(this.trips.length > 0) this.hasTrips = true;
        }
      }, error =>console.error(error));
    }
  }

  updateCar(car: Car){
    console.log("update car to be implemented")
  }

  deleteCar(car: Car){
    console.log("delete car to be implemented")
  }

  onMyCars(){
    this.myCars = !this.myCars;
  }

  onMyTrips(){
    this.myTrips = !this.myTrips;
  }

  onTripDetail(trip: Trip) {
    this._tripService.setSavedTrip(trip);
    this.router.navigate(['/trip-detail']);
  }
}
