import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/login/login.component';
import { TripCardComponent } from './Components/trip-card/trip-card.component';
import { SearchPageComponent } from './Components/search-page/search-page.component';
import { HeaderComponent } from './Components/header/header.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import "cally";
import { RegisterComponent } from './Components/register/register.component';
import { PostTripComponent } from './Components/post-trip/post-trip.component';

@NgModule({
  declarations: [
    AppComponent,
    TripCardComponent,
    SearchPageComponent,
    HeaderComponent,
    PostTripComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
