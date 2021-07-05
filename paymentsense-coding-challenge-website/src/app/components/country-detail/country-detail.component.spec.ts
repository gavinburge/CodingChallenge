import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatCardModule, MatDialogModule, MatListModule, MAT_DIALOG_DATA } from '@angular/material';
import { IGetCountryDetailResponse } from 'src/app/models/queries/get-country-detail-response';
import { CountryDetailComponent } from './country-detail.component';

describe('CountryDetailComponent', () => {
  let component: CountryDetailComponent;
  let fixture: ComponentFixture<CountryDetailComponent>;

  let injectedContent : IGetCountryDetailResponse = {
    name: "France",
    flag: "http://franceflag",
    capitalCity: "paris",
    population: 12345,
    borderingCountries: ["POR", "BEL"],
    currencies: [],
    languages: [],
    timeZones: []
  }

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatCardModule,
        MatListModule,
        MatDialogModule
      ],
      declarations: [CountryDetailComponent],
      providers: [{
        provide: MAT_DIALOG_DATA, useValue: injectedContent
      }]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountryDetailComponent);
    component = fixture.componentInstance;

    component.countryDetail = injectedContent;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render country name and flag in mat card title', () => {
    expect(fixture.nativeElement.querySelector('mat-card-title').textContent).toContain('France');
    expect(fixture.nativeElement.querySelector('mat-card-title').querySelector('img').src).toContain('http://franceflag');
  });
});
