export class InterviewType {

    constructor(id?: string, name?: string) {
        this.id = id;
        this.name = name;

    }

    public id: string;
    public name: string;
}
export class InterviewTypeModel {

    constructor(jobInterviewTypeModel?: InterviewType[], totalCount?: number) {
       this.jobInterviewTypeModel= new Array<InterviewType>();
        this.totalCount = totalCount;
       
    }
    public totalCount: number;
    public jobInterviewTypeModel: InterviewType[];
}
