import { ResponseModel } from "./Response.Model";

export class PagedResponseModel<T> extends ResponseModel<T> {
    public pageNumber : number;
    public pageSize : number;
}
