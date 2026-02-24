export class CandidateBulkUpload {

  constructor(candidateName?: string, jobName?: string, jobTitle?: string, phoneNumber?: string,
    email?: string, status?: string)
  {
  
    this.candidateName = candidateName;
    this.jobName = jobName;
    this.jobTitle = jobTitle;
    this.phoneNumber = phoneNumber;
    this.email = email;
    this.status = status;

    }

  public id: number;
  public candidateName: string;
  public jobName: string;
  public jobTitle: string;
  public phoneNumber: string;
  public email: string;
  public feedback: string;
  public level1ManagerId: number;
  public level1Result: string;
  public level2ManagerId: number;
  public level2Result: string;
  public level3ManagerId: number;
  public level3Result: string;
  public level4ManagerId: number;
  public level4Result: string;
  public status: string;
  public errorMessage: string;
  public isEdit: boolean;
   
}
export class CandidateBulkUploadModel {

  constructor(candidateBulkUploadModel?: CandidateBulkUpload[], totalCount?: number) {
    this.candidateBulkUploadModel = new Array<CandidateBulkUpload>();
        this.totalCount = totalCount;
       
    }
    public totalCount: number;
  public candidateBulkUploadModel: CandidateBulkUpload[];
}

