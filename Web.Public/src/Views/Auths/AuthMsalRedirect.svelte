<script lang="ts">
import { _ } from 'svelte-i18n';

	import { MsalRedirectService } from '../../App/Common/Services/Auth/MSAL/MsalRedirectService';

  let msalRedirectService = new MsalRedirectService();

</script>

  <h1 class="text-orange-700 text-lg text-center font-bold font-serif border-black">{$_('msal_redirect')}</h1>
  <div class="flex justify-items-center justify-center">
    <button
    type="button"
    id="SignIn"
    class="bg-orange-500 hover:bg-orange-700 text-white font-bold py-2 px-4 border border-orange-700 rounded content-center"
    on:click="{() => msalRedirectService.SignIn()}">
    {$_('msal_signin')}
  </button>
   <button
    type="button"
    id="SignIn"
    class="bg-transparent hover:bg-orange-500 text-orange-700 font-semibold hover:text-white py-2 px-4 border border-orange-500 hover:border-transparent rounded "
    on:click="{() => msalRedirectService.SignOut()}">
    {$_('msal_signout')}
  </button>
  </div>

