import { TestBed, async } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController, TestRequest } from '@angular/common/http/testing';
import { ICountryModel } from './../models/country.model';
import { IGetCountriesResponse } from './../models/queries/get-countries-response';
import { IGetCountriesQuery } from './../models/queries/get-countries-query';
import { CountriesApiService } from './countries-api.service';
import { IPaginatedGetCountriesQuery } from '../models/queries/paginated-get-countries-query';
import { IPaginatedGetCountriesResponse } from '../models/queries/paginated-get-countries-response';


describe('countries-api.service', () => {

  let countriesApiService: CountriesApiService;
  let httpTestingController: HttpTestingController;

  beforeEach(async(() => { // 3
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CountriesApiService],
    }).compileComponents();

    countriesApiService = TestBed.get(CountriesApiService);
    httpTestingController = TestBed.get(HttpTestingController)
  }));

  afterEach(() => {
    httpTestingController.verify();
  })

  const getCountryResponseFake: IGetCountriesResponse = {
    countries: [
      { name: 'UK', flag: 'http://1' },
      { name: 'US', flag: 'http://1' },
      { name: 'France', flag: 'http://1' }
    ]
  };

  const paginatedGetCountryResponseFake: IPaginatedGetCountriesResponse = {
    countries: [
      { name: 'UK', flag: 'http://1' },
      { name: 'US', flag: 'http://1' },
      { name: 'France', flag: 'http://1' }
    ],
    pageNumber: 1,
    pageSize: 10,
    totalItems: 250
  };

  it(`should call the get countries endpoint to get a full list of countries`, () => {
    let request: IGetCountriesQuery = {};

    countriesApiService.getCountries(request).subscribe(result => {
      expect(result.countries.length).toEqual(getCountryResponseFake.countries.length);
      expect(result.countries).toEqual(getCountryResponseFake.countries);
    });

    const countriesRequest: TestRequest = httpTestingController.expectOne(`https://localhost:44341/api/v1/countries`);
    expect(countriesRequest.request.method).toEqual('GET');

    countriesRequest.flush(getCountryResponseFake);
  });

  it(`should call the get countries endpoint and receive an error`, () => {
    let request: IGetCountriesQuery = {};

    countriesApiService.getCountries(request).subscribe(result => {
      fail('we are expecting an error for this test')
    },
      (error: string) => {
        expect(error).toEqual('Server encuntered an error')
      });

    const countriesRequest: TestRequest = httpTestingController.expectOne(`https://localhost:44341/api/v1/countries`);
    countriesRequest.flush('whoops we got an error', { status: 500, statusText: "Error here" });
  });

  it(`should call the paginated get countries endpoint to get a paged list of countries`, () => {
    let request: IPaginatedGetCountriesQuery = {
      pageSize: 10,
      pageNumber: 1
    };

    countriesApiService.paginatedGetCountries(request).subscribe(result => {
      expect(result.countries.length).toEqual(paginatedGetCountryResponseFake.countries.length);
      expect(result.countries).toEqual(paginatedGetCountryResponseFake.countries);
      expect(result.pageNumber).toEqual(paginatedGetCountryResponseFake.pageNumber);
      expect(result.pageSize).toEqual(paginatedGetCountryResponseFake.pageSize);
      expect(result.totalItems).toEqual(paginatedGetCountryResponseFake.totalItems);
    });

    const countriesRequest: TestRequest = httpTestingController.expectOne(`https://localhost:44341/api/v1/countries/paginated?pageSize=${request.pageSize}&pageNumber=${request.pageNumber}`);
    expect(countriesRequest.request.method).toEqual('GET');

    countriesRequest.flush(paginatedGetCountryResponseFake);
  });

  it(`should call the get countries endpoint and receive an error`, () => {
    let request: IPaginatedGetCountriesQuery = {
      pageSize: 10,
      pageNumber: 1
    };

    countriesApiService.paginatedGetCountries(request).subscribe(result => {
      fail('we are expecting an error for this test')
    },
      (error: string) => {
        expect(error).toEqual('Server encuntered an error')
      });

    const countriesRequest: TestRequest = httpTestingController.expectOne(`https://localhost:44341/api/v1/countries/paginated?pageSize=${request.pageSize}&pageNumber=${request.pageNumber}`);
    countriesRequest.flush('whoops we got an error', { status: 500, statusText: "Error here" });
  });

});
