import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, timeout, tap } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IGetCountriesQuery } from './../models/queries/get-countries-query';
import { IGetCountriesResponse } from './../models/queries/get-countries-response';

@Injectable({
  providedIn: 'root',
})

export class CountriesApiService {

    constructor(private httpClient: HttpClient){}

    getCountries(request: IGetCountriesQuery) : Observable<IGetCountriesResponse> { 
        return this.httpClient
                    .get<IGetCountriesResponse>('https://localhost:44341/api/v1/countries')
                    .pipe(
                        tap(response => console.log('countries response', JSON.stringify(response))),
                        timeout(30000), 
                        catchError(this.processError));
    }

    processError(error : HttpErrorResponse) {
        console.log(`Error code: ${error.status}, error message: ${error.message}`)
        return throwError('Server encuntered an error');
    }
}