import { Component, Input, OnInit } from '@angular/core';
import { DriverService } from '../../Services/DriverService';
import { User } from '../../Models/user.model';

@Component({
  selector: 'app-trip-card',
  templateUrl: './trip-card.component.html',
  styleUrl: './trip-card.component.css'
})
export class TripCardComponent implements OnInit {
  constructor(private _driverService: DriverService) { }
  @Input() trip: any;
  driver: User | undefined;
  startDay: string = '';
  startHour: string = '';

  ngOnInit(): void {
    let date = new Date(this.trip.startDate);
    if(date){
      this.startDay = date.getDate() + '/' + (date.getMonth()+1) + '/' + date.getFullYear();
      this.startHour = date.getHours() + ':' + date.getMinutes();
    }
    this._driverService.getDriver(this.trip.driverUserName).subscribe(responseMessage =>
      {
        this.driver = responseMessage.data;
        if(this.driver != undefined) this.driver.photoSrc = 'http://localhost:5080/images/driver/' + this.driver.photoSrc;
        console.log(responseMessage);
      }
      , error => console.error(error));
  }
}
