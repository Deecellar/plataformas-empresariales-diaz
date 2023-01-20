
<script lang="ts">
import { ProductModel } from "../../../App/Models/Product.Model";




  export let product : ProductModel;

</script>

    <div>
      <div class="relative px-4 ">
        <div class="bg-white p-6 rounded-lg shadow-lg">
          <h4 class="mt-1 font-semibold text-lg leading-tight truncate">{product.name}</h4>
          <div class="mt-1">
          <img class="object-cover" src="http://barcodes4.me/barcode/c128b/{product.barcode}.png?IsTextDrawn=1&TextSize=15?height=200" alt="This is the barcode of the product {product.name}">
          <p> ${ product.rate } <span class="text-gray-600 text-sm">COP</span></p> 
          </div>
          <p>{product.description}</p>
        </div>
      </div>
    </div>
