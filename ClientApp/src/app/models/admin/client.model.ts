export class Client {

    constructor(id?: string, clientName?: string, firstName?: string, lastName?: string, functionalDepartment?: string, functionalGroup?: string, emailId?: string, contactNo?: string) {

        this.id = id;
        this.fullName = clientName;
        this.department = functionalDepartment;
        this.functionalGroup = functionalGroup;
        this.emailId = emailId;
        this.contactNo = contactNo;
    }
    public id: string;
    public fullName: string;
    public department: string;
    public functionalGroup: string;
    public emailId: string;
    public contactNo: string;
    public title : string;
    public designation : string;
    public band : string;
    public password : string;
}