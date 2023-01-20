<script lang="ts">
  import { KeyedCollection } from './../../../Helpers/KeyedDictionary';
import { beforeUpdate, onMount, setContext } from "svelte";
import { writable } from 'svelte/store';

    let sizes = [0,1,2,3,4,5,6,8,10,12,16,20,24,32];
    let classSize = ["","px","2","3","4","5","6","8","10","12","16","20","24","32"];
    let Margin = new KeyedCollection<number,string>();
    let Width = new KeyedCollection<number,string>();

      Margin.AddRange(sizes,classSize);
      Width.AddRange([0,1,2,3,4,5,6],["","full","1/2","1/3","1/4","1/5","1/6"]);
    
    export let DefaultGridGap= 0;
    export let GridGapSm= 0;
    export let GridGapMd= 0;
    export let GridGapLg= 0;
    export let GridGapXL= 0;
    export let DefaultGridColumn  = 0;
    export let GridColumnSizeSm= 0;
    export let GridColumnSizeMd= 0;
    export let GridColumnSizeLg= 0;
    export let GridColumnSizeXL= 0;
    var itemClasses = writable<string>('');
      setContext("GridContext",itemClasses);
    var containerClassess = '';
    function generateClasses(){
      itemClasses.set('');
      containerClassess = '';
      let sizeSufix = ["","sm:","md:","lg:","xl:"]
      let indexSizesuffix = 0;
      function GetClosest(dict: KeyedCollection<number,string> , goal:number){
        return dict.Item(dict.Keys().reduce(function(prev, curr) {
          return (Math.abs(curr - goal) < Math.abs(prev - goal) ? curr : prev);
        }));
      }
   
      function ContainerPartial(Gap : number){
        let temp : string;
        let itemClassSuffix1 = 'my-'
        let itemClassSuffix2 = 'px-'
        let containerSufix = '-mx-'
        if( (temp = GetClosest(Margin, Gap)) !== "") {
          itemClasses.update(n => n += `${sizeSufix[indexSizesuffix]}${itemClassSuffix1}${temp} `);
          itemClasses.update(n =>n += `${sizeSufix[indexSizesuffix]}${itemClassSuffix2}${temp} `);
          containerClassess += `${sizeSufix[indexSizesuffix++]}${containerSufix}${temp} `;
        }
      };

      function ItemPartial(Column : number){
        let temp : string;

        let itemClassSuffix3 = 'w-'
        if((temp = GetClosest(Margin, Column)) !== "") {
          console.log(temp);

        }
        if((temp = GetClosest(Width, Column)) !== "") {
          itemClasses.update(n => n += `${sizeSufix[indexSizesuffix++]}${itemClassSuffix3}${temp} `);
        }
      };

      [DefaultGridGap, GridGapSm,GridGapMd,GridGapLg,GridGapXL].forEach(x => ContainerPartial(x))
      indexSizesuffix = 0;
      [DefaultGridColumn, GridColumnSizeSm,GridColumnSizeMd,GridColumnSizeLg,GridColumnSizeXL].forEach(x => ItemPartial(x))
    };


    beforeUpdate(() => generateClasses());
    onMount(() => generateClasses());
</script>

<div class="flex flex-wrap overflow-hidden {containerClassess}">
  <slot></slot>  
</div>