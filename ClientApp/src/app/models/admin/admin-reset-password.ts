export class AdminResetPassword {

    constructor(newPassword?: string, confirmPassword?: string, tokenId?: string) {
        this.newPassword = newPassword;
        this.confirmPassword = confirmPassword;
        this.tokenId = tokenId;
    }

    public newPassword: string;
    public confirmPassword: string;
    public tokenId: string;
    public email:string;
}