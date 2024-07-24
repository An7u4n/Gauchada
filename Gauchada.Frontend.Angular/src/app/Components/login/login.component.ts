import { Component } from '@angular/core';
import { FormGroup,FormControl,ReactiveFormsModule } from '@angular/forms';

@Component({
  standalone:true,
  selector: 'app-login',
  imports:[ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {
    loginForm= new FormGroup({
      username:new FormControl(''),
      password:new FormControl('')
      }
    );
  show= false;
  login(){

    }
  register(){

    }
  showPassword(){
    this.show=!this.show;
  }
}
