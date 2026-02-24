export class CandidateJobList {
    public id:string;
    public name:string;
    public email: string;
    public mobile: string;
    public appliedFor: string;
    public resume: string;
    public status: string;
}
export class CandidateJobListModel{
    constructor(candidateModel?: CandidateJobList[], totalCount?: number) {
        this.candidateAppliedJobModel= new Array<CandidateJobList>();
         this.totalCount = totalCount;
        
     }
     public totalCount: number;
     public candidateAppliedJobModel: CandidateJobList[];
}