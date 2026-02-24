export class HrAssessment {
    constructor(id?: string, question?: string, weightage?: string, candidateWeightage?: string) {

        this.question = question;
        this.weightage = weightage;
        this.candidateWeightage = candidateWeightage;
    }
    public id: string;
    public question: string;
    public weightage: string;
    public candidateWeightage: string;
}