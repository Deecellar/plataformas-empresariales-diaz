import { AuthenticationParameters, AuthError, AuthResponse, InteractionRequiredAuthError, UserAgentApplication } from 'msal';
import { Config } from '../../../Config';
import {CallMSGraph } from '../../../Helpers/GraphHelper';

export class MsalRedirectService {
    userName: string;
    MsalObj: UserAgentApplication = new UserAgentApplication(Config.MsalConfig);

    constructor(){
        this.MsalObj.handleRedirectCallback(this.HandleResponse);
    }
    public HandleResponse(error : AuthError, resp: AuthResponse = null) {
        if (resp !== null) {
            this.userName = resp.account.userName;
        }
        else {
            const currentAccounts = this.MsalObj.getAllAccounts();
            if (currentAccounts === null) {
                return;
            } else if (currentAccounts.length > 1) {
                // Add choose account code here
                console.warn("Multiple accounts detected.");
            } else if (currentAccounts.length === 1) {
                this.userName = currentAccounts[0].userName;
            }
        }
    }
    public SignIn() {
        this.MsalObj.loginRedirect(Config.LoginRequest);
    }

    public SignOut() {
        this.MsalObj.logout();
    }

    public GetTokenRedirect(request: AuthenticationParameters) {
        request.account = this.MsalObj.getAllAccounts().find(x => x.userName === this.userName);

        return this.MsalObj.acquireTokenSilent(request).catch(error => {
            console.warn("silent token acquisition fails. acquiring token using popup");
            if (error instanceof InteractionRequiredAuthError) {
                // fallback to interaction when silent call fails
                return this.MsalObj.acquireTokenRedirect(request);
            } else {
                console.warn(error);
            }
        });
    }

    public SeeProfile() {
        this.GetTokenRedirect(Config.LoginRequest).then(response => {
            CallMSGraph(Config.GraphMeEndpoint, (<AuthResponse>response).accessToken, console.log);
        }).catch(error => {
            console.error(error);
        });
    }

    public ReadMail() {
        this.GetTokenRedirect(Config.TokenRequest).then(response => {
            CallMSGraph(Config.GraphMailEndpoint, (<AuthResponse>response).accessToken,console.log);
        }).catch(error => {
            console.error(error);
        });
    }
}