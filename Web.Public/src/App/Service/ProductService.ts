import { BaseService } from '../Common/Services/BaseService';
import { ApiEndpoints } from '../Common/ApiEndpoints';
import { ProductModel } from '../Models/Product.Model';
import { CreateProduct } from '../Filters/Products/CreateProduct';
import { UpdateProduct } from '../Filters/Products/UpdateProduct';
import { ResponseModel } from '../Common/Models/Response.Model';

export class ProductService extends BaseService
{
    public  GetProducts() : Promise<ResponseModel<ProductModel[]>>{
        return  this.Get<ProductModel[]>(ApiEndpoints.ProductV1);
    }
    public GetProduct(id : number) : Promise<ResponseModel<ProductModel>>{
        return this.Get<ProductModel>(`${ApiEndpoints}/${id}`);
    }
    public AddProduct(model : CreateProduct) : Promise<ResponseModel<number>> {

        return this.Post<CreateProduct,number>(ApiEndpoints.ProductV1,model);
    }
    public DeleteProduct(id : number){
        this.http(`${ApiEndpoints}/${id}`,  {method: 'delete'});
    }
    public UpdateProduct(model : UpdateProduct) : Promise<ResponseModel<ProductModel>>{
        return this.Post<UpdateProduct,ProductModel>(ApiEndpoints.ProductV1,model);
    }
}
