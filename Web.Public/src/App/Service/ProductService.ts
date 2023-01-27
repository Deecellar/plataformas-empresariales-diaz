import { ApiEndpoints, BaseService,  CreateProduct, ProductModel, ResponseModel, UpdateProduct, UserModel } from "..";

export class ProductService extends BaseService {
    declare headers : Headers;
    declare user : UserModel
    constructor() {
        super();
    }
    public GetProducts(): Promise<ResponseModel<ProductModel[]>> {
        return this.Get<ProductModel[]>(ApiEndpoints.ProductV1);
    }
    public GetProduct(id: number): Promise<ResponseModel<ProductModel>> {
        return this.Get<ProductModel>(`${ApiEndpoints}/${id}`);
    }
    public AddProduct(model: CreateProduct): Promise<ResponseModel<number>> {

        return this.Post<CreateProduct, number>(ApiEndpoints.ProductV1, model);
    }
    public DeleteProduct(id: number) {
        this.http(`${ApiEndpoints}/${id}`, { method: 'delete' });
    }
    public UpdateProduct(model: UpdateProduct): Promise<ResponseModel<ProductModel>> {
        return this.Post<UpdateProduct, ProductModel>(ApiEndpoints.ProductV1, model);
    }
}
