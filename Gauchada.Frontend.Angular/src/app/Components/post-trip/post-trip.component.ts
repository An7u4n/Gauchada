import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TripService } from '../../Services/TripService';
import { CarService } from '../../Services/CarService';
import { DriverService } from '../../Services/DriverService';
import { User } from '../../Models/user.model';
import { BehaviorSubject } from 'rxjs';
import { UserService } from '../../Services/UserService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-trip',
  templateUrl: './post-trip.component.html',
  styleUrl: './post-trip.component.css'
})
export class PostTripComponent implements OnInit {
  cars: any[] = [];
  postError: string | null = null;
  formData = {
    origin: '',
    destination: '',
    hour: '',
    carPlate: ''
  }
  driver!: User;
  constructor(private _tripService: TripService, private _carService: CarService, private _userService: UserService, private router: Router) {
  }

  ngOnInit(): void {
    this.driver = this._userService.getLoggedUser();
    this._carService.getUserCars(this.driver.userName).subscribe(carRet => {
      if(carRet && carRet.data){
        this.cars = carRet.data;
        if(this.cars.length > 0)
          this.formData.carPlate = this.cars[0].carPlate;
      }
    }, error =>console.error(error));

  }

  postTrip(event: Event, calendar: any) {
    if(this.checkErrors(calendar.value)){

      event.preventDefault();
      this._tripService.postTrip(this.formData.origin, this.formData.destination, calendar.value+'T'+this.formData.hour+':00', this.driver.userName, this.formData.carPlate)
        .subscribe(responseMessage => {

          this._tripService.setSavedTrip(responseMessage.data);
          alert("Gauchada Created With Success");
          this.router.navigate(['/trip-detail']);
        }, error => this.postError = error);
    }
  }

  checkErrors(date: any): boolean {
    if(this.formData.origin == '' || this.formData.destination == '' || this.formData.hour == '' || this.formData.carPlate == ''){
      this.postError = "All fields are required";
      return false;
    }
    if(date == null){
      this.postError = "Date is required";
      return false;
    }
    if(new Date(date+'T'+this.formData.hour) < new Date()){
      this.postError = "Date must be in the future";
      return false;
    }
    return true;
  }
}
