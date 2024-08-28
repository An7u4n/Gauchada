import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { TripService } from '../../Services/TripService';
import { CarService } from '../../Services/CarService';

@Component({
  selector: 'app-post-trip',
  templateUrl: './post-trip.component.html',
  styleUrl: './post-trip.component.css'
})
export class PostTripComponent implements OnInit {
  constructor(private _tripService: TripService, private _carService: CarService) { }
  cars: any[] = [];
  postError: string | null = null;
  formData = {
    origin: '',
    destination: '',
    hour: '',
    carPlate: ''
  }
  userName: string = 'TestUser';

  ngOnInit(): void {
    this._carService.getUserCars(this.userName).subscribe(carRet => {
      if(carRet && carRet.data){
        this.cars = carRet.data;
        console.log(this.cars);
      }
    }, error =>console.error(error));
  }

  postTrip(event: Event, calendar: any) {
    event.preventDefault();
    this._tripService.postTrip(this.formData.origin, this.formData.destination, calendar.value+'T'+this.formData.hour+':00', this.userName, this.formData.carPlate)
      .subscribe(responseMessage => console.log(responseMessage), error => this.postError = error);
    // Add post method to the service
  }
}
