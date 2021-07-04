import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatCardModule, MatDialogModule, MatListModule, MAT_DIALOG_DATA } from '@angular/material';
import { IGetCountryDetailResponse } from 'src/app/models/queries/get-country-detail-response';
import { CountryDetailComponent } from './country-detail.component';

describe('CountryDetailComponent', () => {
  let component: CountryDetailComponent;
  let fixture: ComponentFixture<CountryDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatCardModule,
        MatListModule,
        MatDialogModule
      ],
      declarations: [CountryDetailComponent],
      providers: [{
        provide: MAT_DIALOG_DATA, useValue: () => {
          let response: IGetCountryDetailResponse = {
            name: "France",
            flag: "http://",
            capitalCity: "paris",
            population: 12345,
            borderingCountries: ["POR", "BEL"],
            currencies: [],
            languages: [],
            timeZones: []
          }

          return response;
        }
      }]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CountryDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
