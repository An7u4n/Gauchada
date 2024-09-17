import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { DriverService } from '../../Services/DriverService';
import { UserService } from '../../Services/UserService';

@Component({
  standalone:true,
  selector: 'app-login',
  imports:[ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent implements OnInit {
  constructor(private router: Router, private _loginService: UserService) { }
  loginForm= new FormGroup({
      username:new FormControl(''),
      password:new FormControl(''),
      userType:new FormControl('')
    }
  );
  show = false;
  errorMessage = '';

  ngOnInit(): void {
    if(this._loginService.isLogged())
      this.router.navigate(['/trips']);
  }

  login() {
    let username = this.loginForm.value.username;
    let password = this.loginForm.value.password;
    let userType = this.loginForm.value.userType;
  
    if (username != undefined && password != undefined && userType != undefined && username.length > 0 && userType.length > 0) {
      if(userType == 'driver'){
      this._loginService.loginDriver(username, password).subscribe(isLoggedIn => {
        if (!isLoggedIn) {
          this.errorMessage = 'User or password wrong';
        } else {
          this.router.navigate(['/trips']);
        }
      }, () => this.errorMessage = 'User or password wrong');
    } else {
      this._loginService.loginPassenger(username, password).subscribe(isLoggedIn => {
        if (!isLoggedIn) {
          this.errorMessage = 'User or password wrong';
        } else {
          this.router.navigate(['/trips']);
        }
      },() => this.errorMessage = 'User or password wrong');
    }
    } else {
      this.errorMessage = 'Please fill all the fields';
    }
  }

  register(){
    this.router.navigate(['/register']);
  }

  showPassword(){
    console.log(this._loginService.isLogged());
  }
}
