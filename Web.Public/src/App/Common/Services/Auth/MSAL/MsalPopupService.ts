import { AuthenticationParameters, AuthResponse, InteractionRequiredAuthError, UserAgentApplication } from 'msal';
import { Config } from 'App';
import {CallMSGraph } from 'App';

export class MsalPopupService {
    userName: string;
    MsalObj: UserAgentApplication = new UserAgentApplication(Config.MsalConfig);

    constructor(){
        this.LoadPage();
    }
    public LoadPage() {
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

    public HandleResponse(resp: AuthResponse) {
        if (resp !== null) {
            this.userName = resp.account.userName;
        }
        else {
            this.LoadPage();
        }
    }
    public SignIn() {
        this.MsalObj.loginPopup(Config.LoginRequest).then(this.HandleResponse).catch(error => {
            console.error(error);
        });
    }

    public SignOut() {
        this.MsalObj.logout();
    }

    public GetTokenPopUp(request: AuthenticationParameters) {
        request.account = this.MsalObj.getAllAccounts().find(x => x.userName === this.userName);

        return this.MsalObj.acquireTokenSilent(request).catch(error => {
            console.warn("silent token acquisition fails. acquiring token using popup");
            if (error instanceof InteractionRequiredAuthError) {
                // fallback to interaction when silent call fails
                return this.MsalObj.acquireTokenPopup(request).then(tokenResponse => {
                    console.log(tokenResponse);
                    return tokenResponse;
                }).catch(error => {
                    console.error(error);
                });
            } else {
                console.warn(error);
            }
        });
    }

    public SeeProfile() {
        this.GetTokenPopUp(Config.LoginRequest).then(response => {
            CallMSGraph(Config.GraphMeEndpoint, (<AuthResponse>response).accessToken, console.log);
        }).catch(error => {
            console.error(error);
        });
    }

    public ReadMail() {
        this.GetTokenPopUp(Config.TokenRequest).then(response => {
            CallMSGraph(Config.GraphMailEndpoint, (<AuthResponse>response).accessToken,console.log);
        }).catch(error => {
            console.error(error);
        });
    }
}