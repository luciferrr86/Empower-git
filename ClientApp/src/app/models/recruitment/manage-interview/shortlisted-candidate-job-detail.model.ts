export class CandidateJobDetails {

  constructor(name?: string, emailId?: string, mobileNo?: string, appliedFor?: string, score?: string, comment?: string, jobApplicationId?:string) {

        this.candidateName = name;
        this.email = emailId;
        this.mobile = mobileNo;
        this.appliedFor = appliedFor;
        this.score = score;
    this.comment = comment;
    this.jobApplicationId = jobApplicationId;

    }

    public candidateName: string;
    public email: string;
    public mobile: string;
    public appliedFor: string;
    public score: string;
    public comment: string;
  public id: string;
  public jobApplicationId: string;
    public questionAnswerList: ManagerKpi[];

}

export class ManagerKpi {

    constructor(jobQuestionId?: string, question?: string, weightage?: string, obtainedWeightage?: string) {

        this.jobQuestionId = jobQuestionId;
        this.question = question;
        this.weightage = weightage;
        this.obtainedWeightage = obtainedWeightage;
    }

    public jobQuestionId: string;
    public question: string;
    public weightage: string;
    public levelSkillQuestionId:string;
    public obtainedWeightage: string;

}
export class ManagerKpiModel {
    constructor(jobHRKpiList?: ManagerKpi[]) {

        this.managerKpiList = new Array<ManagerKpi>();
    }

    public managerKpiList: ManagerKpi[];
}
