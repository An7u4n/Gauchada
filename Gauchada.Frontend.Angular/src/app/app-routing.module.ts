import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/login/login.component';
import { TripCardComponent } from './Components/trip-card/trip-card.component';
import { SearchPageComponent } from './Components/search-page/search-page.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'tripcard', component: TripCardComponent },
  { path: 'search-trips', component: SearchPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
