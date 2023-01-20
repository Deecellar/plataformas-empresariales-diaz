import type {HeaderResponseModel } from '../Headers/HeaderResponse.Model';

export class ResponseModel<T> {
    public header : HeaderResponseModel;
    public message : string;
    public data : T;
    public errors: string[];
}