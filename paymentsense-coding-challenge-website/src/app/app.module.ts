import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaymentsenseCodingChallengeApiService } from './services';
import { HttpClientModule } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MaterialModule } from './material.module'
import { MatProgressSpinnerModule, MatTableModule  } from '@angular/material';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { CountriesApiService } from './services/countries-api.service';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HomeComponent } from './components/home/home.component';
import { CountriesComponent } from './components/countries/countries.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    CountriesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FontAwesomeModule,
    MaterialModule,
    BsDropdownModule.forRoot(),
    MatProgressSpinnerModule,
    MatTableModule 
  ],
  providers: [
    PaymentsenseCodingChallengeApiService,
    CountriesApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
