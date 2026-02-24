export class Candidate{
    public id:string;
    public name:string;
    public email: string;
    public mobile: string;
    public appliedFor: string;
    public resume: string;
    public status: string;
}
export class CandidateModel{
    constructor(candidateModel?: Candidate[], totalCount?: number) {
        this.candidateModel= new Array<Candidate>();
         this.totalCount = totalCount;
        
     }
     public totalCount: number;
     public candidateModel: Candidate[];

}