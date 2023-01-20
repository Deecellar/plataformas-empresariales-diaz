<!-- TODO https://dev.to/vibhanshu909/how-to-create-a-full-featured-modal-component-in-svelte-and-trap-focus-within-474i citar con normas APA (?) -->
<!-- if you're not using typescript, remove lang="ts" attribute from the script tag -->
<script context="module" lang="ts">
  // for passing focus on to the next Modal in the queue.
  // A module context level object is shared among all its component instances. [Read More Here](https://svelte.dev/tutorial/sharing-code)
  const modalList : Array<{ addEventListener: (arg0: string,arg1: { (e: any): void; (e: any): void; }) => void; focus: () => void; removeEventListener: (arg0: string,arg1: { (e: any): void; (e: any): void; }) => void; }> = []
</script>

<script>import Backdrop from "./Backdrop.svelte";
import { _ } from "svelte-i18n";

    let isOpen = false;

    function triggerModal(){
        isOpen = !isOpen;
    }

  function keydown(e: { stopPropagation: () => void; key: string; }) {
    e.stopPropagation()
    if (e.key === 'Escape') {
      triggerModal();
    }
  }
  function transitionend(e: { target: any; }) {
    const node = e.target
    node.focus()
  }
  function modalAction(node: { addEventListener: (arg0: string,arg1: { (e: any): void; (e: any): void; }) => void; focus: () => void; removeEventListener: (arg0: string,arg1: { (e: any): void; (e: any): void; }) => void; }) {
    const returnFn : Array<Function> = []
    // for accessibility
    if (document.body.style.overflow !== 'hidden') {
      const original = document.body.style.overflow
      document.body.style.overflow = 'hidden'
      returnFn.push(() => {
        document.body.style.overflow = original
      })
    }
    node.addEventListener('keydown', keydown)
    node.addEventListener('transitionend', transitionend)
    node.focus()
    modalList.push(node)
    returnFn.push(() => {
      node.removeEventListener('keydown', keydown)
      node.removeEventListener('transitionend', transitionend)
      modalList.pop()
      // Optional chaining to guard against empty array.
      modalList[modalList.length - 1]?.focus()
    })
    return {
      destroy: () => returnFn.forEach((fn) => fn()),
    }
  }
</script>

<style> 

  h1 {
    @apply opacity-50;
  }

</style>

<slot name="trigger">
  <!-- fallback trigger to open the modal -->
  <button class="bg-transparent hover:bg-orange-500 text-orange-700 font-semibold hover:text-white py-2 px-4 border border-orange-500 hover:border-transparent rounded" on:click={triggerModal }>{$_('modal_trigger')}</button>
</slot>
{#if isOpen}
<Backdrop  open ={isOpen} on:click={triggerModal}>
  <div class="fixed left-0 top-0 w-full h-screen flex items-center justify-center opacity-100 not-focus-within:transition-opacity not-focus-within:duration-75" use:modalAction tabindex="0">

    <div class="z-10 max-w-screen/2 rounded bg-white overflow-hidden ">
      <slot name="header">
        <!-- fallback -->
        <div>
          <h1>{$_('modal_heading')}</h1>
        </div>
      </slot>

      <div class="max-h-screen/2 overflow-auto">
        <slot name="content"/>
      </div>

      <slot name="footer">
        <!-- fallback -->
        <div>
          <h1>{$_('modal_footer')}</h1>
          <button on:click={triggerModal}>Close</button>
        </div>
      </slot>
    </div>

  </div>
</Backdrop>

{/if}
