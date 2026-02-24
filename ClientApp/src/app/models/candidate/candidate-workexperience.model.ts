export class JobWorkExperience {
    constructor(id?: string, companyName?: string, empID?: string, designation?: string, dor?: Date, doj?: Date, profileDesc?: string) {

        this.id = id;
        this.companyName = companyName;
        this.designation = designation;
        this.dor = dor;
        this.doj = doj;
        this.profileDesc = profileDesc;
        this.empID = empID;
    }
    public id: string;
    public companyName: string;
    public designation: string;
    public dor: Date;
    public doj: Date;
    public profileDesc: string;
    public empID: string;
}