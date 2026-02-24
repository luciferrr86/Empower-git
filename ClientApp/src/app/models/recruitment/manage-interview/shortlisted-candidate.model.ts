import { Candidate } from "../candidate-view/candidate-list.model";

export class CandidateModel extends Candidate{
 
    public scheduleDateTime: string;
}
export class ShortListedCandidateModel{
    constructor(shortListedCandidateModel?: CandidateModel[], totalCount?: number) {
        this.candidateModel= new Array<CandidateModel>();
         this.totalCount = totalCount;
        
     }
     public totalCount: number;
     public candidateModel: CandidateModel[];
}