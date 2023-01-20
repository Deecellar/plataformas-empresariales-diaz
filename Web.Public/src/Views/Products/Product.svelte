<script lang="ts">
    import "../../main.css";
import { CreateProduct } from '../../App/Filters/Products/CreateProduct';
import { ProductModel } from '../../App/Models/Product.Model';
import { ProductService } from '../../App/Service/ProductService';
import ProductView from './Components/ProductView.svelte';
import { _ } from "svelte-i18n";
    const productsService = new ProductService();
    var createProduct  = new CreateProduct();
    var products = [new ProductModel()];
    products = [];
    let result : number;
    async function getProducts(){
        let a = new ProductService();
        let responses = a.GetProducts();
        products = (await responses).data;
    }
    async function addProduct(){
         result = (await productsService.AddProduct(createProduct)).data;
    }
    getProducts();
</script>

    <div class="flex justify-items-center justify-center ">
        <form class="flex flex-col" >
            <input class="text-input"  bind:value="{createProduct.name}" placeholder='{$_('product_name')}' />
            <input  class="text-input"  bind:value="{createProduct.barcode}" placeholder='{$_('product_barcode')}'/>
            <textarea class="text-input" bind:value="{createProduct.description}" placeholder='{$_('product_description')}'/>
            <input class="text-input" type="number" bind:value="{createProduct.rate}" placeholder="{$_('product_rate')}"/>
            <button type="button" class="bg-transparent hover:bg-orange-500 text-orange-700 font-semibold hover:text-white py-2 px-4 w-2/3 border border-orange-500 hover:border-transparent rounded " on:click="{() => addProduct()}">{$_('product_submit')}</button>
        </form>
        <button class="bg-transparent hover:bg-orange-500 text-orange-700 font-semibold hover:text-white py-2 px-4 border border-orange-500 hover:border-transparent rounded" on:click="{() => getProducts()}" >{$_('product_update')}</button>
        <div class="flex flex-wrap -mx-2 overflow-hidden sm:-mx-3 md:-mx-3 lg:-mx-3 xl:-mx-2">
            {#each products as product}
            <ProductView product={product}/>
            {/each}
            <p>{$_('product_result')}: {result}</p>
        </div>
    </div>




