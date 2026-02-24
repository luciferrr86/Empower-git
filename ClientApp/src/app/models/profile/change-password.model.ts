export class ChangePassword {
    constructor(currentPassword?:string,newPassword?:string,confirmNewPassword?:string)
    {
        this.currentPassword=currentPassword;
        this.newPassword=newPassword;
        this.confirmNewPassword=confirmNewPassword;
    }
    public currentPassword:string;
    public newPassword:string;
    public confirmNewPassword:string;
}
