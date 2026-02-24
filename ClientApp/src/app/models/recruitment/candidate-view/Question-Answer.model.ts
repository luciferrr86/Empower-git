export class QuestionAnswerViewModel{

    public scriningList:ScriningQuestion[];
    public hrkpiList:HrKpiQuestion[];
    public skillInterviewList:SkillInterviewQuestion[];

}
export class ScriningQuestion{
    constructor(id?: string, question?: string, answer?: string, acctualAnswer?: string) {

        this.question = question;
        this.answer = answer;
        this.acctualAnswer = acctualAnswer;
    }
    public id: string;
    public question: string;
    public answer: string;
    public acctualAnswer: string;

}
export class HrKpiQuestion{
    constructor(id?: string, question?: string, weightage?: string, acctualWeightage?: string) {

        this.question = question;
        this.weightage = weightage;
        this.acctualWeightage = acctualWeightage;
    }
    public id: string;
    public question: string;
    public weightage: string;
    public acctualWeightage: string;

}
export class SkillInterviewQuestion{
    constructor(id?: string, question?: string, weightage?: string, acctualWeightage?: string) {

        this.question = question;
        this.weightage = weightage;
        this.acctualWeightage = acctualWeightage;
    }
    public id: string;
    public question: string;
    public weightage: string;
    public acctualWeightage: string;
}