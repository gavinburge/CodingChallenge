import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatCardModule, MatDialogModule, MatListModule, MatPaginatorModule, MatTableModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CountriesApiService } from 'src/app/services/countries-api.service';
import { IconService } from 'src/app/services/icon.service';
import { MockCountriesApiService } from 'src/app/testing/mock-countries-api-service';
import { CountryDetailComponent } from '../country-detail/country-detail.component';

import { CountriesComponent } from './countries.component';

describe('CountriesComponent', () => {
  let component: CountriesComponent;
  let fixture: ComponentFixture<CountriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatCardModule,
        MatListModule,
        FontAwesomeModule,
        MatTableModule,
        MatPaginatorModule,
        HttpClientTestingModule,
        MatDialogModule,
        BrowserAnimationsModule
      ],
      declarations: [
        CountriesComponent,
        CountryDetailComponent
      ],
      providers: [ 
          IconService,
          { provide: CountriesApiService, useClass: MockCountriesApiService } 
        ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
