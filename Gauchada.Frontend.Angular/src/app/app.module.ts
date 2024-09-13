import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/login/login.component';
import { TripCardComponent } from './Components/trip-card/trip-card.component';
import { SearchPageComponent } from './Components/search-page/search-page.component';
import { HeaderComponent } from './Components/header/header.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import "cally";
import { RegisterComponent } from './Components/register/register.component';
import { PostTripComponent } from './Components/post-trip/post-trip.component';
import { AddCarComponent } from './Components/add-car/add-car.component';
import { ProfileDashboardComponent } from './Components/profile-dashboard/profile-dashboard.component';
import { JwtInterceptor } from './Services/JWTInterceptor';
import { TripCardPopupComponent } from './Components/trip-card-popup/trip-card-popup.component';

@NgModule({
  declarations: [
    AppComponent,
    TripCardComponent,
    SearchPageComponent,
    HeaderComponent,
    PostTripComponent,
    AddCarComponent,
    ProfileDashboardComponent,
    TripCardPopupComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
