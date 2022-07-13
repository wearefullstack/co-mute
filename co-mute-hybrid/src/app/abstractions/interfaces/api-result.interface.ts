export interface IApiResult<T> { 
    success: boolean;
    error : string;
    result : T;
}