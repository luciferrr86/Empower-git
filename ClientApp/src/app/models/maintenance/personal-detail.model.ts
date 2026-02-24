import { DropDownList } from "../common/dropdown";

export class PersonalDetail {
  constructor(
    id?: string,
    fullName?: string,
    city?: string,
    contactNo?: string,
    country?: string,
    currentAddress?: string,
    dob?: Date,
    workEmailId?: string,
    emailId?: string,
    emergencyContactName?: string,
    emergencyContactNo?: string,
    emergencyContactRelation?: string,
    fatherName?: string,
    gender?: string,
    idProofType?: DropDownList[],
    idProofDetail?: string,
    motherName?: string,
    nationality?: string,
    officialContactNo?: string,
    permanentAddress?: string,
    maritalStatus?: DropDownList[],
    state?: string,
    zipCode?: string,
    panNumber?: string,
    currentCity?: string,
    currentState?: string,
    currentZipCode?: string,
    currentCountry?: string
  ) {
    this.id = id;
    this.fullName = fullName;
    this.city = city;
    this.contactNo = contactNo;
    this.country = country;
    this.currentAddress = currentAddress;
    this.dob = dob;
    this.workEmailId = workEmailId;
    this.emailId = emailId;
    this.emergencyContactName = emergencyContactName;
    this.emergencyContactNo = emergencyContactNo;
    this.emergencyContactRelation = emergencyContactRelation;
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
    this.panNumber = panNumber;
    this.currentCity = currentCity;
    this.currentState = currentState;
    this.currentCountry = currentCountry;
    this.currentZipCode = currentZipCode;
  }

  public id: string;
  public fullName: string;
  public city: string;
  public contactNo: string;
  public country: string;
  public currentAddress: string;
  public dob: Date;
  public workEmailId: string;
  public emailId: string;
  public emergencyContactName: string;
  public emergencyContactNo: string;
  public emergencyContactRelation: string;
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
  public panNumber: string;
  public currentCity: string;
  public currentState: string;
  public currentZipCode: string;
  public currentCountry: string;
}
