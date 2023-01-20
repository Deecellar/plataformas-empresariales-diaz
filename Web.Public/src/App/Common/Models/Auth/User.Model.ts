export class UserModel {
    public id : string;
    public userName : string;
    public email : string;
    public roles : string[];
    public isVerified : boolean;
    public jwtToken : string;
    public refreshToken : string;
    
}