import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { TripCardComponent } from './Components/trip-card/trip-card.component';
import { SearchPageComponent } from './Components/search-page/search-page.component';
import { RegisterComponent } from './Components/register/register.component';
import { PostTripComponent } from './Components/post-trip/post-trip.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'posttriptest', component: PostTripComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'tripcard', component: TripCardComponent },
  { path: 'trips', component: SearchPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
