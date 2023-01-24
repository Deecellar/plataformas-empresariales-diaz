import { ApiEndpoints } from "../../../ApiEndpoints"
import { BaseService } from "../../BaseService"
import type { ResponseModel } from '../../../Models/CommonResponse/Response.Model'
import type { UserRegisterModel } from '../../../Models/Auth/User.Register.Model'
import type { UserLoginModel } from '../../../Models/Auth/User.Login.Model'
import type { UserForgotPasswordModel } from '../../../Models/Auth/User.ForgotPassword.Model'
import type { UserResetPasswordModel } from '../../../Models/Auth/User.ResetPassword.Model'

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