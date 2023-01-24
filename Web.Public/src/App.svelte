<script lang="ts">
  import Login from './App/Common/Views/Auth/Login.svelte';
    import Register from './App/Common/Views/Auth/Register.svelte';
    import Modal from './App/Common/Views/Components/Modal.svelte';
import svelteLogo from './assets/svelte.svg'
  import Counter from './lib/Counter.svelte'

  import { RegisterLocalLangs } from './App/Common/Helpers/LocalizationHelper';
  import Router from 'svelte-spa-router';
  import type { RouteDefinition } from 'svelte-spa-router';
  import {link} from 'svelte-spa-router';
	import { Config } from './App/Common/Config';
  import { isLoading, locale, locales, _, init } from 'svelte-i18n';
    import { active } from './App/Common/Helpers/active';


init({
    fallbackLocale: 'en',
    initialLocale: 'en',
})

let routes : RouteDefinition  = Config.Routes;
let links : {path: string, name: string}[];
locale.subscribe(() => links =  [{path: "/", name: $_('path_home')},{path: "/product", name: $_('path_product')}]);

RegisterLocalLangs();
console.log(routes)
</script>
<main>
  <div>

    {#if $isLoading}
  Please wait...
  {:else}
  <select bind:value={$locale}>
    {#each $locales as locale}
      <option value={locale}>{locale}</option>
    {/each}
  </select>
  <div>
    <nav class="p-4">
      <ul class="flex">
        {#each links as l}
        <li class="mr-1" use:active={{path: l.path , className: '-mb-px'} }>
          <a
            class="bg-white inline-block py-2 px-4 text-blue-500
            hover:text-blue-800 font-semibold"
            use:link
            use:active={{path: l.path, className: 'border-l border-t border-r rounded-t text-blue-700'}}
              href={l.path}>
            {l.name}
          </a>
        </li>
        {:else}
        <p>Wha</p>
        {/each}
        <li class="mr-1">
        <Modal >
          <h3 slot="header">{$_('login_header')}</h3>
          <div slot="content">
            <Login registerLink={"/register"} forgotPassword={"/forgot-password"} />
            
          </div>
        </Modal>
        </li>
  
      </ul>
  
    </nav>
  
    <Router { routes }/>
  </div>
  {/if}
  
  </div>
  
</main>

