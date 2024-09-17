import { Component } from '@angular/core';
import { CarService } from '../../Services/CarService';
import { Car } from '../../Models/car.model';
import { max } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styleUrl: './add-car.component.css'
})
export class AddCarComponent {
  formData = {
    plate: '',
    brand: '',
    model: '',
    year: '',
    color: '',
    maxPassengers: 5,
  }
  postError: string = '';

  constructor(private _carService: CarService, private router: Router) { }

  postCar(e: Event) {
    e.preventDefault();
    this.validateForm();
    if(this.postError === '') {
      this._carService.postCar(new Car(this.formData.plate,this.formData.brand,this.formData.model+' '+this.formData.year,this.formData.color,'',this.formData.maxPassengers))
      .subscribe(postResult => {
        if(postResult.success === true) {
          alert('Car Succesfully Added');
          this.router.navigate(['']);
        } else {
          this.postError = 'Error al agregar el auto, ', postResult.message;
        }
      });
    
    }
  }

  validateForm() {
    if (this.formData.plate.length < 6) {
      this.postError = 'CarPlate must be 6 or 7 characters long';
    } else if (this.formData.brand.length < 2) {
      this.postError = 'Please enter a valid car brand';
    } else if (this.formData.model.length < 2) {
      this.postError = 'Please enter a valid car model';
    } else if (this.formData.year.length < 4) {
      this.postError = 'Please enter a valid year';
    } else if (this.formData.color.length < 2) {
      this.postError = 'Car color must be at least 2 characters long';
    } else {
      this.postError = '';
    }
  }
}
