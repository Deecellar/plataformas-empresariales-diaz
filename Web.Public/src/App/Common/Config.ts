import { AuthenticationParameters, Configuration, Logger, LogLevel} from 'msal'; // This is for msal (azure)
import type {RouteDefinition } from 'svelte-spa-router';
import Product from '../../Views/Products/Product';
import ProductDetail from "../../Views/Products/ProductDetail";
import ForgotPassword from "./Views/Auth/ForgotPassword";
import Home from "../../Views/Home/Home";
import NotFound from "../../Views/NotFound";

export class Config {
    public static ApiURl : string = (import.meta.env.MODE === 'development' ? (import.meta.env.MODE === 'test' ? 'https://localhost:9001/' : 'https://localhost:9001/') : 'webapitemplate.lldragon.net');

    //MSAL CONFIG START
    public static MsalConfig : Configuration = {
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
            logger: new Logger(() => (level : LogLevel, message : String, containsPii : boolean) => {
                if (containsPii) {
                    return;
                }
                switch (level) {
                    case LogLevel.Error:
                        console.error(message);
                        return;
                    case LogLevel.Info:
                        console.info(message);
                        return;
                    case LogLevel.Verbose:
                        console.debug(message);
                        return;
                    case LogLevel.Warning:
                        console.warn(message);
                        return;
                }
            })
            
        },
        
    };
    public static LoginRequest : AuthenticationParameters = {
        scopes: ["User.Read"]
    };
    public static TokenRequest : AuthenticationParameters = {
        scopes: ["User.Read", "Mail.Read"],
        forceRefresh: false // Set this to "true" to skip a cached token and go to the server to get a new token
    };
    public static GraphMeEndpoint: "https://graph.microsoft.com/v1.0/me";
    public static GraphMailEndpoint: "https://graph.microsoft.com/v1.0/me/messages";
    // MSAL CONFIG END
    // Routing Start 
    public static Routes : RouteDefinition =  {
        '/' : Home,
        
        '/product/' : Product,
        '/product/:id' : ProductDetail,
        '/forgot-password/' : ForgotPassword,
        '*' : NotFound
    }
    //Routing End
}