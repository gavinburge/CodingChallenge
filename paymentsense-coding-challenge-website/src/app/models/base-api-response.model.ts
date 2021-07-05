import { IErrorModel } from "./error.model";

export interface IBaseApiResponseModel<T> {
    success : boolean;
    data : T;
    errors : IErrorModel[];
}