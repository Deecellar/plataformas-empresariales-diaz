<script lang="ts">
import { MsalPopupService } from "../../App/Common/Services/Auth/MSAL/MsalPopupService";
import { _ } from 'svelte-i18n'; 


  let msalPopupService = new MsalPopupService();

</script>

  <h1 class="text-orange-700 text-lg text-center font-bold font-serif border-black">{$_('msal_popup')}</h1>
  <div class="flex   justify-items-center justify-center">
    <button
    type="button"
    id="SignIn"
    class="bg-orange-500 hover:bg-orange-700 text-white font-bold py-2 px-4 border border-orange-700 rounded content-center"
    on:click="{() => msalPopupService.SignIn()}">
    {$_('msal_signin')}
  </button>
   <button
    type="button"
    id="SignIn"
    class="bg-transparent hover:bg-orange-500 text-orange-700 font-semibold hover:text-white py-2 px-4 border border-orange-500 hover:border-transparent rounded "
    on:click="{() => msalPopupService.SignOut()}">
    {$_('msal_signout')}
  </button>
  </div>

