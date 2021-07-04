import { ICurrencyModel } from './../currency.model';
import { ILanguageModel } from './../language.model';

export interface IGetCountryDetailResponse {
    name : string;
    flag : string;
    population : number;
    timeZones : string[]
    currencies : ICurrencyModel[]
    languages : ILanguageModel[]
    capitalCity : string
    borderingCountries : string[]
}