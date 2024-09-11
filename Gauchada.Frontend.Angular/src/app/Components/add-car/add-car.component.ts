import { Component } from '@angular/core';
import { CarService } from '../../Services/CarService';
import { Car } from '../../Models/car.model';
import { max } from 'rxjs';

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
    maxPassengers: 0,
  }
  postError: string = '';

  constructor(private _carService: CarService) {}

  postCar(e: Event) {
    e.preventDefault();
    this.validateForm();
    if(this.postError === '') {
      this._carService.postCar(new Car(this.formData.plate,this.formData.brand,this.formData.model+' '+this.formData.year,this.formData.color,'',this.formData.maxPassengers))
      .subscribe(postResult => {
        if(postResult.success === true) {
          alert('Auto agregado correctamente');
        } else {
          this.postError = 'Error al agregar el auto, ', postResult.message;
        }
      });
    
    }
  }

  validateForm() {
    if (this.formData.plate.length < 6) {
      this.postError = 'La patente debe tener 6 o 7 caracteres';
    } else if (this.formData.brand.length < 2) {
      this.postError = 'Por favor ingrese una marca';
    } else if (this.formData.model.length < 2) {
      this.postError = 'Por favor ingrese un modelo';
    } else if (this.formData.year.length < 4) {
      this.postError = 'Por favor ingrese un año valido';
    } else if (this.formData.color.length < 2) {
      this.postError = 'El color debe tener al menos 2 caracteres';
    } else {
      this.postError = '';
    }
  }
}
