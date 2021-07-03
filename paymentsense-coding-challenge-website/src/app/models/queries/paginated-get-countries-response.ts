import { ICountryModel } from '../country.model';

export interface IPaginatedGetCountriesResponse {
    countries : ICountryModel[];
    pageNumber: number;
    pageSize: number;
    totalItems: number;
}