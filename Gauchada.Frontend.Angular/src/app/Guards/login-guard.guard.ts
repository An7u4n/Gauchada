import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../Services/UserService';
import { inject } from '@angular/core';

export const loginGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router);

  if (userService.isLogged()) {
    if (state.url.includes('login') || state.url.includes('register')) {
      router.navigate(['/trips']);
      return false;
    }
    return true;
  } else {
    if (state.url.includes('login') || state.url.includes('register')) {
      return true;
    }
    router.navigate(['/login']);
    return false;
  }
};
