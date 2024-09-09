import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { TripCardComponent } from './Components/trip-card/trip-card.component';
import { SearchPageComponent } from './Components/search-page/search-page.component';
import { RegisterComponent } from './Components/register/register.component';
import { PostTripComponent } from './Components/post-trip/post-trip.component';
import { loginGuard } from './Guards/login-guard.guard';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent, canActivate: [loginGuard]},
  { path: 'register', component: RegisterComponent, canActivate: [loginGuard] },
  { path: 'add-trip', component: PostTripComponent, canActivate: [loginGuard] },
  { path: 'trips', component: SearchPageComponent, canActivate: [loginGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
