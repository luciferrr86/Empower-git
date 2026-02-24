import { PersonalDetail } from "./personal-detail.model";

export class ProfessionalDetail {
    constructor(id?: string, companyName?: string, empId?:string, designation?: string, dor?: Date, doj?: Date, profileDesc?: string) {

        this.id = id;
        this.companyName = companyName;
        this.designation = designation;
        this.dor = dor;
        this.doj = doj;
        this.profileDesc = profileDesc;
        this.empID=empId;
    }
    public id: string;
    public companyName: string;
    public designation: string;
    public dor: Date;
    public doj: Date;
    public profileDesc: string;
    public empID:string;
}
export class ProfessionalDetailModel {
    constructor(totalCount?: number, professionalDetailModel?: ProfessionalDetail[]) {
        this.totalCount = totalCount;
        this.professionalDetailModel = new Array<ProfessionalDetail>();

    }
    public totalCount: number;
    public professionalDetailModel: ProfessionalDetail[];
}