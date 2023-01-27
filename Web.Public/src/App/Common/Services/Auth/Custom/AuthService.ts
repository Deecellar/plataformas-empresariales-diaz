import { ApiEndpoints } from "App"
import { BaseService } from "App"
import type { ResponseModel } from 'App'
import type { UserRegisterModel } from 'App'
import type { UserLoginModel } from 'App'
import type { UserForgotPasswordModel } from 'App'
import type { UserResetPasswordModel } from 'App'

// This uses an API different than the BaseService, so it doesn't extend it
export class AuthService extends BaseService {
//TODO Add Authorization Logic here, We still need to define a router
  ConfirmEmail(code : string) : Promise<ResponseModel<string>> {
    console.log(JSON.stringify(code));
    return this.Get<string>(`${ApiEndpoints.AccountConfirmEmail}/?userId=${this.user.id}&code=${code}`);
  }
  Register(UserRegister : UserRegisterModel) : Promise<ResponseModel<string>> {
    console.log(JSON.stringify(UserRegister));
    return this.Post<UserRegisterModel,string>(ApiEndpoints.AccountRegister, UserRegister);
  }
  Authenticate(UserLogin : UserLoginModel) : Promise<ResponseModel<string>>  {
    console.log(JSON.stringify(UserLogin));
    return this.Post<UserLoginModel,string>(ApiEndpoints.AccountAuthenticate,UserLogin);
  }
  ForgotPassword(UserForgotInfo : UserForgotPasswordModel) : Promise<ResponseModel<string>> {
    console.log(JSON.stringify(UserForgotInfo));
    return this.Post<UserForgotPasswordModel,string>(ApiEndpoints.AccountForgetPassword,UserForgotInfo);
  }

  ResetPassword(UserResetInfo : UserResetPasswordModel) : Promise<ResponseModel<string>> {
    console.log(JSON.stringify(UserResetInfo));
    return this.Post<UserResetPasswordModel,string>(ApiEndpoints.AccountResetPassword,UserResetInfo);   
  }

}