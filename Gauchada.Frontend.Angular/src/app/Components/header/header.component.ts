import { Component } from '@angular/core';
import { User } from '../../Models/user.model';
import { UserService } from '../../Services/UserService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  user: User;
  isDropdownOpen = false;
  constructor(private _loginService: UserService, private router: Router) {
    this.user = this._loginService.getLoggedUser();
  }

  onLogout(){
    this._loginService.logout();
    this.router.navigate(['/login']);
  }

  onTripCreate(){
    this.router.navigate(['/add-trip']);
  }

  onLogoClick(){
    this.router.navigate(['/trips']);
  }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}
