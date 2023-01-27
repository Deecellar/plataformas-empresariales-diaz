<script lang="ts">
    import { parse,ParsedQs } from 'qs';
    import { querystring } from 'svelte-spa-router';
    import { AuthService } from "App";
import { UserResetPasswordModel } from "App";
import { _ } from 'svelte-i18n';
        let reset = new UserResetPasswordModel();
        let authService = new AuthService();
        let query = parse($querystring as string) as ParsedQs;
        
        reset.email = String(query["email"]);
        reset.Token = String(query["token"]);
    </script>
    
    <div class="flex justify-items-center justify-center ">
        <form class="flex flex-col" on:submit|preventDefault={() => authService.ResetPassword(reset)}> 
            <label>
                {$_('auth_email')}: <input  class="text-input" type="email" bind:value={reset.email} disabled/> 
            </label>
            <label>
                {$_('auth_password')}: <input  class="text-input" type="password" bind:value={reset.password}/> 
            </label>
            <label>
                {$_('auth_confirm_password')}: <input  class="text-input" type="password" bind:value={reset.confirmPassword}/> 
            </label>
            <input type="submit" value="{$_('auth_reset_password')}" />
        </form>
    </div>