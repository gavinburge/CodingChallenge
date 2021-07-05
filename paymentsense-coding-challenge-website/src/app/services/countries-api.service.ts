import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, timeout, tap } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IGetCountriesQuery } from './../models/queries/get-countries-query';
import { IGetCountriesResponse } from './../models/queries/get-countries-response';
import { IPaginatedGetCountriesQuery } from '../models/queries/paginated-get-countries-query';
import { IPaginatedGetCountriesResponse } from '../models/queries/paginated-get-countries-response';
import { IGetCountryDetailQuery } from '../models/queries/get-country-detail-query';
import { IGetCountryDetailResponse } from '../models/queries/get-country-detail-response';
import { IBaseApiResponseModel } from '../models/base-api-response.model';

@Injectable({
  providedIn: 'root',
})

export class CountriesApiService {

    constructor(
        @Inject('BASE_API_URL') private baseUrl: string,
        private httpClient: HttpClient){}

    getCountries(request: IGetCountriesQuery) : Observable<IBaseApiResponseModel<IGetCountriesResponse>> { 
        return this.httpClient
                    .get<IBaseApiResponseModel<IGetCountriesResponse>>(`${this.baseUrl}/api/v1/countries`)
                    .pipe(
                        tap(response => console.log('countries response', JSON.stringify(response))),
                        timeout(30000), 
                        catchError(this.processError));
    }

    paginatedGetCountries(request: IPaginatedGetCountriesQuery) : Observable<IBaseApiResponseModel<IPaginatedGetCountriesResponse>> { 
        return this.httpClient
                    .get<IBaseApiResponseModel<IPaginatedGetCountriesResponse>>(`${this.baseUrl}/api/v1/countries/paginated?pageSize=${request.pageSize}&pageNumber=${request.pageNumber}`)
                    .pipe(
                        tap(response => console.log('countries response', JSON.stringify(response))),
                        timeout(30000), 
                        catchError(this.processError));
    }

    getCountryDetail(request: IGetCountryDetailQuery) : Observable<IBaseApiResponseModel<IGetCountryDetailResponse>> { 
        return this.httpClient
                    .get<IBaseApiResponseModel<IGetCountryDetailResponse>>(`${this.baseUrl}/api/v1/countries/detail?countryName=${request.countryName}`)
                    .pipe(
                        tap(response => console.log('country detail', JSON.stringify(response))),
                        timeout(30000), 
                        catchError(this.processError));
    }

    processError(error : HttpErrorResponse) {
        console.log(`Error code: ${error.status}, error message: ${error.message}`)
        return throwError('Server encuntered an error');
    }
}