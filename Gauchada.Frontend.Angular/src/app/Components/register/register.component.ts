import { Component } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../../Models/user.model';
import { DriverService } from '../../Services/DriverService';
import { PassengerService } from '../../Services/PassengerService';

@Component({
  standalone:true,
  selector: 'app-register',
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  constructor(private router: Router, private _driverService: DriverService, private _passengerService: PassengerService) { }
  image: File = new File([], '');
  registerForm = new FormGroup({
      username:new FormControl(''),
      password:new FormControl(''),
      name:new FormControl(''),
      lastName:new FormControl(''),
      email:new FormControl(''),
      phoneNumber:new FormControl(''),
      birth:new FormControl(''),
      userType:new FormControl('')
    }
  );
  show= false;

  login(){
    this.router.navigate(['/login']);
  }

  register(){
    if(!this.registerForm.invalid){
      const user = new User(
        this.registerForm.value.username!,
        this.registerForm.value.name!,
        this.registerForm.value.lastName!,
        this.registerForm.value.email!,
        this.registerForm.value.birth!,
        this.registerForm.value.phoneNumber!,
        ''
      );
      if(this.registerForm.value.userType == 'driver'){
        this._driverService.postDriver(user, this.image).subscribe(returnValue => console.log(returnValue), error => console.error(error));
  
      } else if(this.registerForm.value.userType == 'passenger') {
        this._passengerService.postDriver(user, this.image).subscribe(returnValue => console.log(returnValue), error => console.error(error));
      }
    }
    
  }
  showPassword(){
    this.show=!this.show;
  }

  onFileChange(event: any){
    event.preventDefault();
    this.image = event.target.files[0];
  }
}
