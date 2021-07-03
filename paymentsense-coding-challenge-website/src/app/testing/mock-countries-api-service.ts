import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { IPaginatedGetCountriesQuery } from "../models/queries/paginated-get-countries-query";
import { IPaginatedGetCountriesResponse } from "../models/queries/paginated-get-countries-response";

@Injectable({
    providedIn: 'root'
  })
  export class MockCountriesApiService {
  
      getPaginatedCountries(request: IPaginatedGetCountriesQuery) : Observable<IPaginatedGetCountriesResponse> { 
          let result :  IPaginatedGetCountriesResponse = {
                  pageSize: 5,
                  pageNumber: 1,
                  totalItems: 3,
                  countries: [
                      { name: 'UK', flag: 'mockUkFlag' },
                      { name: 'US', flag: 'mockUSFlag' },
                      { name: 'France', flag: 'mockFranceFlag' },
                      { name: 'Germany', flag: 'mockGermanyFlag' },
                      { name: 'Italy', flag: 'mockItalyFlag' },
                  ]
              };
  
          return of(result);
      }
  }