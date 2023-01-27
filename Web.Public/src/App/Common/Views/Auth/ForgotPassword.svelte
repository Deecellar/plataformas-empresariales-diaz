<script lang="ts">

    import { AuthService } from "App";
import { UserForgotPasswordModel } from "App";
import { _ } from "svelte-i18n";
        let forgot = new UserForgotPasswordModel();
        let authService = new AuthService();
    
    </script>
    
    <div class="flex justify-items-center justify-center ">
        <form class="flex flex-col" on:submit|preventDefault={() => authService.ForgotPassword(forgot)}> 
            <label>
                {$_('auth_email')}: <input class="text-input" type="email" bind:value={forgot.email}/> 
            </label>
            <input type="submit" value="{$_('auth_password_request')}" />
        </form>
    </div>