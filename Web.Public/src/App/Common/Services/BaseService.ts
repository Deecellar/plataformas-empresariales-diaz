import { Config } from '../Config';
import type { UserModel } from '../Models/Auth/User.Model';
import type { ResponseModel } from '../Models/CommonResponse/Response.Model';
import type { PagedResponseModel } from '../Models/CommonResponse/PagedResponse.Model';
export abstract class BaseService {
    public headers : Headers;
    public ApiRoot = Config.ApiURl;
    public user : UserModel
    constructor() {
        this.headers = new Headers(
        );
    }

    async http<TResponse>(path: string, config: RequestInit): Promise<ResponseModel<TResponse>> {

        let fullPath = `${this.ApiRoot}${path}`
        this.addAuthorization();
        this.headers.delete('Access-Control-Allow-Origin');
        this.headers.append("Access-Control-Allow-Origin",'*');
        let init = {headers: this.headers, ...config}
        const request = new Request( fullPath, init)
        const response = await fetch(request)
      
        if(!response.ok) {
      
          throw new Error(JSON.stringify({name: response.status, message: response.statusText}))
      
        }
      
        // may error if there is no body, return empty array
      
        return response.json().catch(() => ({}))
      
      }
      
      public async Get<TResponse>(path: string, config?: RequestInit): Promise<ResponseModel<TResponse>> {
      
        const init = {method: 'get', ...config};
        this.removeContentType();
        return this.http(path, init);
      
      }
      
      public async Post<TModel,TResponse>(path: string, body: TModel, config?: RequestInit): Promise<ResponseModel<TResponse>> {
      
        const init = {method: 'post', body: JSON.stringify(body), ...config};
        this.addJsonType();
        return this.http(path, init);
      
      }
      
      public async Put<TModel,TResponse>(path: string, body: TModel, config?: RequestInit): Promise<ResponseModel<TResponse>> {
      
        const init = {method: 'put', body: JSON.stringify(body), ...config};
        this.addJsonType();
        return this.http(path, init);
      
      }
      async httpPaged<TResponse>(path: string, config: RequestInit): Promise<PagedResponseModel<TResponse>> {

        const request = new Request(path, config)
      
        const response = await fetch(request)
      
        if(!response.ok) {
      
          throw new Error(JSON.stringify({name: response.status, message: response.statusText}))
      
        }
      
        // may error if there is no body, return empty array
      
        return response.json().catch(() => ({}))
      
      }
      
      public async GetPaged<TResponse>(path: string, config?: RequestInit): Promise<PagedResponseModel<TResponse>> {
      
        const init = {method: 'get', ...config};
      
        this.removeContentType();
        return this.httpPaged(path, init);
      
      }
      
      //TODO: Add File Uploading

      //TODO: Add File Downloading

       addAuthorization() {
         this.headers.delete("Authorization");
          this.headers.append("Authorization",`${localStorage.getItem('token')}`);
      }

      addJsonType(){
        this.removeContentType();
        this.headers.append("Content-Type","application/json;charset=UTF-8")
      }

      removeContentType() {
        this.headers.delete("Content-Type")
      }
}