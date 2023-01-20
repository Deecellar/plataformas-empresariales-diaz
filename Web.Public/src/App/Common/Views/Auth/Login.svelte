<script lang="ts">

import { UserLoginModel } from "../../Models/Auth/User.Login.Model"
import { AuthService } from "../../Services/Auth/Custom/AuthService";
import {link} from 'svelte-spa-router'
import { _ } from "svelte-i18n";
    let login = new UserLoginModel();
    let authService = new AuthService();
    export let registerLink  : string;
    export let forgotPassword : string;
    console.log(registerLink+forgotPassword);
</script>

<div class="flex justify-items-center justify-center ">
    <form class="flex flex-col" on:submit|preventDefault={() => authService.Authenticate(login)}> 
        <label>
            {$_('auth_email')}: <input class="text-input" type="email" bind:value={login.email}/> 
        </label>
        <label>
            {$_('auth_password')}: <input class="text-input"  type="password" bind:value={login.password}/> 
        </label>
        <input type="submit" value="{$_('auth_login')}" />
    </form>
    <a href={registerLink} use:link >{$_('auth_register')}</a>
    <a href={forgotPassword} use:link >{$_('auth_forgot_password_url')}</a>
</div>