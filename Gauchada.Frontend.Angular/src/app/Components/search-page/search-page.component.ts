import { Component } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { TripService } from '../../Services/TripService';

@Component({
  selector: 'app-search-page',
  templateUrl: './search-page.component.html',
  styleUrl: './search-page.component.css'
})
export class SearchPageComponent {
  searchError: string | null = null;
  searchSuccess: boolean = false;
  trips: any[] = [];
  formData = {
    origin: '',
    destination: ''
  }
  constructor(private _tripService: TripService) { }

  searchTrips(event: Event, tripDay: any) {
    this.searchError = '';
    event.preventDefault();
    if(this.checkErrors(tripDay)){
      this._tripService.tripSearch(this.formData.origin, this.formData.destination, tripDay.value).subscribe(trips => {
        if(trips.success == true){
          this.searchSuccess = true;
          this.trips = trips.data;
        }
      }, error => { console.log(error.error.message); this.searchError = 'No trips found between'+this.formData.origin+' and '+this.formData.destination+' on '+tripDay.value; });
    }
  }

  checkErrors(tripDay: any) : boolean{
    if(this.formData.origin == '' || this.formData.destination == '' || tripDay.value == ''){
      this.searchError = 'Please fill all the fields';
      return false;
    } else if(new Date(tripDay.value) < new Date){
      this.searchError = 'Please select a valid date';
      return false;
    }
    return true;
  }
}
