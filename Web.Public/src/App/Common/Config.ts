import * as msal from 'msal'; // This is for msal (azure)
import type { RouteDefinition } from 'svelte-spa-router';
import ForgotPassword from "./Views/Auth/ForgotPassword.svelte";
import Register from "./Views/Auth/Register.svelte";
import Home from "../../lib/Home.svelte";
import NotFound from "../../lib/NotFound.svelte";
export class Config {
    public static ApiURl: string = (import.meta.env.MODE === 'development' ? 'http://localhost:5000/' : '0.0.0.0');

    //MSAL CONFIG START
    public static MsalConfig: msal.Configuration = {
        auth: {
            clientId: "109d0d9e-2790-4e23-a858-723ce45408ed",
            authority: "https://login.microsoftonline.com/common/",
            redirectUri: "http://localhost:8080/",
        },
        cache: {
            cacheLocation: "sessionStorage", // This configures where your cache will be stored
            storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
        },
        system: {
            logger: new msal.Logger(() => (level: msal.LogLevel, message: String, containsPii: boolean) => {
                if (containsPii) {
                    return;
                }
                switch (level) {
                    case msal.LogLevel.Error:
                        console.error(message);
                        return;
                    case msal.LogLevel.Info:
                        console.info(message);
                        return;
                    case msal.LogLevel.Verbose:
                        console.debug(message);
                        return;
                    case msal.LogLevel.Warning:
                        console.warn(message);
                        return;
                }
            })

        },

    };
    public static LoginRequest: msal.AuthenticationParameters = {
        scopes: ["User.Read"]
    };
    public static TokenRequest: msal.AuthenticationParameters = {
        scopes: ["User.Read", "Mail.Read"],
        forceRefresh: false // Set this to "true" to skip a cached token and go to the server to get a new token
    };
    public static GraphMeEndpoint: "https://graph.microsoft.com/v1.0/me";
    public static GraphMailEndpoint: "https://graph.microsoft.com/v1.0/me/messages";
    // MSAL CONFIG END
    // Routing Start 
    public static Routes: RouteDefinition = {
        '/': Home,

        '/forgot-password/': ForgotPassword,
        '/register/': Register,
        '*': NotFound
    }
    //Routing End
}