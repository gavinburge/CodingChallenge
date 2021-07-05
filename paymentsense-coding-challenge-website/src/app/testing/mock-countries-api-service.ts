import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { IBaseApiResponseModel } from "../models/base-api-response.model";
import { IPaginatedGetCountriesQuery } from "../models/queries/paginated-get-countries-query";
import { IPaginatedGetCountriesResponse } from "../models/queries/paginated-get-countries-response";

@Injectable({
    providedIn: 'root'
})

export class MockCountriesApiService {

    paginatedGetCountries(request: IPaginatedGetCountriesQuery): Observable<IBaseApiResponseModel<IPaginatedGetCountriesResponse>> {
        let result: IBaseApiResponseModel<IPaginatedGetCountriesResponse> = {
            success: true,
            errors: [],
            data: {
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
            }
    };

        return of(result);
    }
}