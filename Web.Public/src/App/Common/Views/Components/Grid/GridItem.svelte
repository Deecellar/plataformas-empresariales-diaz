<script lang="ts">
    import { getContext } from "svelte";
    const itemClass = getContext<any>("GridContext")
</script>

<div class="{$itemClass} overflow-hidden">
    <slot></slot>
</div>