import { Component, OnInit } from '@angular/core';
import { User } from '../../Models/user.model';
import { UserService } from '../../Services/UserService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit{
  user!: User;
  isDropdownOpen: boolean = false;
  isDriver: boolean = false;
  constructor(private _loginService: UserService, private router: Router) {

  }

  ngOnInit() {
    this.user = this._loginService.getLoggedUser();
    let userType = this._loginService.getLoggedUserType();
    if (userType == 'driver') {
      this.isDriver = true;
      this.user.photoSrc = 'http://localhost:5080/images/driver/' + this.user.photoSrc;
    } else {
      this.user.photoSrc = 'http://localhost:5080/images/passenger/' + this.user.photoSrc;
    }
  }

  onProfileClick(){
    this.router.navigate(['/profile']);
  }

  onLogout(){
    this._loginService.logout();
    this.router.navigate(['/login']);
  }

  onTripCreate(){
    this.router.navigate(['/add-trip']);
  }

  onAddCar(){
    this.router.navigate(['/add-car']);
  }

  onLogoClick() {
    if (this.isDriver)
      this.router.navigate(['/add-trip']);
    else
      this.router.navigate(['/trips']);
  }
  
  onTripSearch(){
    this.router.navigate(['/trips']);
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}
