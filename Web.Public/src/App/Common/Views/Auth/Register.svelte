<script lang="ts">

    import { AuthService } from "App";
import { UserRegisterModel } from "App";
import {link} from 'svelte-spa-router';
import { _ } from "svelte-i18n";

        let register = new UserRegisterModel();
        let authService = new AuthService();
        export let loginLink  : string;
        
</script>
    
    <div class="flex justify-items-center justify-center ">
        <form class="flex flex-col" on:submit|preventDefault={() => authService.Register(register)}> 
            <label>
                {$_('auth_fname')}: <input class="text-input" type="text" bind:value={register.firstName}/> 
            </label>
            <label>
                {$_('auth_lname')}: <input class="text-input" type="text" bind:value={register.lastName}/> 
            </label>
            <label>
                {$_('auth_uname')}: <input class="text-input" type="text" bind:value={register.userName}/> 
            </label>
            <label>
                {$_('auth_email')}: <input class="text-input" type="email" bind:value={register.email}/> 
            </label>
            <label>
                {$_('auth_password')}: <input class="text-input" type="password" bind:value={register.password}/> 
            </label>
            <label>
                {$_('auth_confirm_password')}: <input class="text-input" type="password" bind:value={register.confirmPassword}/> 
            </label>
            <input type="submit" value="{$_('auth_register')}" />
        </form>
        <a href={loginLink} use:link >{$_('auth_login')}</a>
    </div>