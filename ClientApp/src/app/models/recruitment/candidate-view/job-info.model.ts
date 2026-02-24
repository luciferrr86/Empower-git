export class JobInfo {
    constructor(id?:string,jobTitle?: string, dateOfAppliction?: Date, applictionType?: string, status?: string) {

        this.jobTitle = jobTitle;
        this.dateOfAppliction = dateOfAppliction;
        this.applictionType = applictionType;
        this.status = status;

    }
    public id:string;
    public jobTitle: string;
    public dateOfAppliction: Date;
    public applictionType: string;
    public status: string;
}