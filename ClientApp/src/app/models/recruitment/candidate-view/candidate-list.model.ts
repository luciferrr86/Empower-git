export class InterviewLevelModel{
    public id:string;
    public name:string;
}

export class Candidate{
    public id:string;
    public name:string;
    public email: string;
    public mobile: string;
    public appliedFor: string;
    public resume: string;
  public status: string;
  public levelId: string;
}
export class CandidateModel{
    constructor(candidateModel?: Candidate[],interviewLevelModel?: InterviewLevelModel[], totalCount?: number) {
        this.candidateModel= new Array<Candidate>();
        this.interviewLevelModel= new Array<InterviewLevelModel>();
         this.totalCount = totalCount;
        
     }
     public totalCount: number;
     public position: string;
     public candidateModel: Candidate[];
     public interviewLevelModel: InterviewLevelModel[];
}
