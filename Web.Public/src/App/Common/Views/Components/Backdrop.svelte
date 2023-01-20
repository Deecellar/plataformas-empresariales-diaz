<script lang="ts">
    export let open = false;
</script>

<div class="bg-black fixed flex top-0 left-0 right-0 bottom-0 items-center content-center z-50 w-full h-full bg-opacity-50   {open ? 'opacity-100 transition-opacity' : 'opacity-0 transition-opacity'}">
    <div>
        <slot />
      </div>
</div>
