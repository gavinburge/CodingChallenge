import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize, map } from "rxjs/operators";
import { ICountryModel } from "src/app/models/country.model";
import { IGetCountriesQuery } from "src/app/models/queries/get-countries-query";
import { CountriesApiService } from "src/app/services/countries-api.service";

// data source used to be reactive to data changes
export class CountriesDataSource implements DataSource<ICountryModel> {

    private countriesSubject = new BehaviorSubject<ICountryModel[]>(null);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private countriesApiService: CountriesApiService) {}

    connect(collectionViewer: CollectionViewer): Observable<ICountryModel[]> {
        return this.countriesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.countriesSubject.complete();
        this.loadingSubject.complete();
    }
  
    loadCountries() {
        let request : IGetCountriesQuery = {};

        this
            .countriesApiService
                .getCountries(request)
                .pipe(map(response => response.countries),
                        catchError(() => of([])),
                        finalize(() => this.loadingSubject.next(false))            )
            .subscribe(countries => this.countriesSubject.next(countries));
    }  
}