import { DropDownList } from "../../common/dropdown";
import { InterviewLevelOption } from './interview-level.model';

export class JobCreate {
    constructor( jobTypeId?: string, jobTitle?: string, jobLocation?: string, experience?: string, noOfvacancies?: number
      , jobRequirements?: string, jobDescription?: string, jdAvailable?: string, managerIdL1?: string[],managerIdL2?: string[],managerIdL3?: string[], salaryRange?: string, currency?: string) {


        this.jobTypeId = jobTypeId;
        this.jobTitle = jobTitle;
        this.jobLocation = jobLocation;
        this.experience = experience;
        this.noOfvacancies = noOfvacancies;
        this.jobRequirements = jobRequirements;
        this.jobDescription = jobDescription;
        this.salaryRange = salaryRange;
      this.currency = currency;
      this.jdAvailable = jdAvailable;

    }
    public id: string;
    public jobTypeId: string;
    public jobType: DropDownList[];
    public jobTitle: string;
    public jobLocation: string;
    public experience: string;
    public noOfvacancies: number;
    public jobRequirements: string;
    public jobDescription: string;
    public jobVacancyLevel :InterviewLevelOption[];
    public hiringManager: DropDownList[];
    public salaryRange: string;
  public currency: string;
  public jdAvailable: string;
    public jobTypeList: DropDownList[];
    public managerList: DropDownList[];


}
