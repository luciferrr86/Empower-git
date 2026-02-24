export class CandidateViewModel {
    constructor(id?: string, fullName?: string, email?: string, phoneNumber?: string, password?: string, rememberMe?: boolean) {

        this.id = id;
        this.fullName = fullName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.password = password;
        this.rememberMe = rememberMe;

    }
    public id: string;
    public fullName: string;
    public email: string;
    public phoneNumber: string;
    public password: string;
    public rememberMe: boolean;
}