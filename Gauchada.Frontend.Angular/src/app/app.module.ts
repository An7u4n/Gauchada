import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Components/login/login.component';
import { TripCardComponent } from './Components/trip-card/trip-card.component';
import { SearchPageComponent } from './Components/search-page/search-page.component';
import { HeaderComponent } from './Components/header/header.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import "cally";

@NgModule({
  declarations: [
    AppComponent,
    TripCardComponent,
    SearchPageComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
