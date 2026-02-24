export class JobDetailModel {
    constructor(jobTitle?: string, jobType?: string, jobTypeId?: string, experience?: string, noOfvacancies?: string, jobLocation?: string, salaryRange?: string, publishedDate?: string, jobDescription?: string, jobRequirements?: string) {

        this.jobTitle = jobTitle;
        this.jobType = jobType;
        this.jobTypeId = jobTypeId;
        this.experience = experience;
        this.noOfvacancies = noOfvacancies;
        this.jobLocation = jobLocation;
        this.salaryRange = salaryRange;
        this.publishedDate = publishedDate;
        this.jobDescription = jobDescription;
        this.jobRequirements = jobRequirements;

    }
    public jobVacancyId: string;
    public jobTitle: string;
    public jobType: string;
    public jobTypeId: string;
    public experience: string;
    public noOfvacancies: string;
    public jobLocation: string;
    public salaryRange: string;
    public publishedDate: string;
    public jobDescription: string;
    public jobRequirements: string;

}