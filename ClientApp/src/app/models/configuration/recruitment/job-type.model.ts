export class JobType {

    constructor(id?: string, name?: string) {
        this.id = id;
        this.name = name;

    }

    public id: string;
    public name: string;
}
export class JobTypeModel {

    constructor(jobTypeModel?: JobType[], totalCount?: number) {
       this.jobTypeModel= new Array<JobType>();
        this.totalCount = totalCount;
       
    }
    public totalCount: number;
    public jobTypeModel: JobType[];
}
