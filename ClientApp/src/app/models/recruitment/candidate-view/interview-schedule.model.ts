
export class InterViewSchedule {
    constructor(id?: string, candidateName?: string, date?: Date, time?: string) {

        this.id = id;
        this.date = date;
        this.time = time;


    }
    public name: string;
    public jobInterviewTypeId: string
    public jobApplicationId:string;
    public interviewId: string;
    public interviewTypeId: string;
  public managerId: string;
  public jobCandidateUrl: string="";

    public id: string;
    public date: Date;
    public time: string;
    public interviewMode: string;
    public levelId : string;
    public isLevelCompleted:boolean;
    public managerIdList: string[];
}
