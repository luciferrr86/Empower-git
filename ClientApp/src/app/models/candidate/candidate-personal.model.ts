import { DropDownList } from "../common/dropdown";

export class JobCandidateProfile {
    constructor(id?: string, fullName?: string,emailId?:string, city?: string, contactNo?: string, country?: string, currentAddress?: string, dob?: Date
        , resume?: string, coverLetter?: string, picture?: string, fatherName?: string, profilePic?: string, skillSet?: string, userId?: string
        , gender?: string, idProofType?: DropDownList[], idProofDetail?: string, motherName?: string, nationality?: string, officialContactNo?: string
        , permanentAddress?: string, maritalStatus?: DropDownList[], state?: string, zipCode?: string) {

        this.id = id;
        this.userId = userId;
        this.fullName = fullName;
        this.city = city;
        this.contactNo = contactNo;
        this.country = country;
        this.currentAddress = currentAddress;
        this.dob = dob;
        this.resume = resume;
        this.coverLetter = coverLetter;
        this.picture = picture;
        this.profilePic = profilePic;
        this.fatherName = fatherName;
        this.gender = gender;
        this.idProofType = idProofType;
        this.idProofDetail = idProofDetail;
        this.maritalStatus = maritalStatus;
        this.motherName = motherName;
        this.nationality = nationality;
        this.officialContactNo = officialContactNo;
        this.permanentAddress = permanentAddress;
        this.state = state;
        this.zipCode = zipCode;
        this.skillSet = skillSet;
        this.emailId=emailId;

    }

    public id: string;
    public userId: string;
    public fullName: string;
    public city: string;
    public contactNo: string;
    public country: string;
    public currentAddress: string;
    public dob: Date;
    public resume: string;
    public coverLetter: string;
    public picture: string;
    public fatherName: string;
    public gender: string;
    public idProofType: DropDownList[];
    public idProofDetail: string;
    public maritalStatus: DropDownList[];
    public motherName: string;
    public nationality: string;
    public officialContactNo: string;
    public permanentAddress: string;
    public state: string;
    public zipCode: string;
    public profilePic: string;
    public skillSet: string;
    public emailId:string;
}
