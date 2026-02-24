import { DropDownList } from "../../common/dropdown";

export class Vacancy {
  constructor(id?: string, jobType?: string, jobTitle?: string, jobLocation?: string, jobExperience?: string, jobPublished?: boolean, jdAvailable?: string) {

        this.id = id;
        this.jobType = jobType;
        this.jobTitle = jobTitle;
        this.jobLocation = jobLocation;
        this.jobExperience = jobExperience;
    this.jobPublished = jobPublished;
    this.jdAvailable = jdAvailable;
    }
    public id: string;
    public jobType: string;
    public jobTitle: string;
    public jobLocation: string;
    public jobExperience: string;
  public jobPublished: boolean;
  public jdAvailable: string;
}
export class VacancyModel {
    constructor(jobVacancyModel?: Vacancy[], totalCount?: number) {
        this.jobVacancyModel = new Array<Vacancy>();
        this.totalCount = totalCount;

    }
    public totalCount: number;
    public jobVacancyModel: Vacancy[];
    public jobTypeList: DropDownList[];
    public managerList: DropDownList[];
}
