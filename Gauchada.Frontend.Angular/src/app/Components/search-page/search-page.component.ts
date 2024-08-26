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
  formData = {
    origin: '',
    destination: ''
  }
  constructor(private _tripService: TripService) { }

  searchTrips(event: Event) {
    event.preventDefault();

    this._tripService.tripSearch(this.formData.origin, this.formData.destination).subscribe(trips => {
      console.log(trips);
    }, error => { console.log(error.error.message); this.searchError = error.error.message; });
  }
}
