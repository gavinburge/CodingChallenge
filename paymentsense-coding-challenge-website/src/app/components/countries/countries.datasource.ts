import {CollectionViewer, DataSource} from "@angular/cdk/collections";
import { BehaviorSubject, Observable, of } from "rxjs";
import { catchError, finalize, map } from "rxjs/operators";
import { ICountryModel } from "src/app/models/country.model";
import { IGetCountriesQuery } from "src/app/models/queries/get-countries-query";
import { IPaginatedGetCountriesQuery } from "src/app/models/queries/paginated-get-countries-query";
import { IPaginatedGetCountriesResponse } from "src/app/models/queries/paginated-get-countries-response";
import { CountriesApiService } from "src/app/services/countries-api.service";

// data source used to be reactive to data changes
export class CountriesDataSource implements DataSource<ICountryModel> {

    private countriesSubject = new BehaviorSubject<ICountryModel[]>(null);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();
    public totalItems : number;

    constructor(private countriesApiService: CountriesApiService) {}

    connect(collectionViewer: CollectionViewer): Observable<ICountryModel[]> {
        return this.countriesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.countriesSubject.complete();
        this.loadingSubject.complete();
    }
  
    loadCountries(pageNumber : number, pageSize : number) {
        let request : IPaginatedGetCountriesQuery = 
        {
            pageSize : pageSize,
            pageNumber : pageNumber
        };

        this
            .countriesApiService
                .paginatedGetCountries(request)
                .pipe(
                        catchError(() => of([])),
                        finalize(() => this.loadingSubject.next(false)))
                .subscribe((response:IPaginatedGetCountriesResponse) => {
                    this.totalItems = response.totalItems;
                    this.countriesSubject.next(response.countries)
                });
    }  
}