import { DropDownList } from "../../common/dropdown";

export class CandidateApplicationViewModel {
    constructor(jobInformationModel?: JobInformationModel) {
        this.jobInformationModel = new JobInformationModel();
    }

    public jobInformationModel: JobInformationModel;
    public listInterviewScheduleModel: InterviewScheduleModel[];
    public questionAnswerModel: QuestionAnswerModel;
    public managerList: DropDownList[];
    public interviewTypeList: DropDownList[];
    public interviewMode: DropDownList[];
}

export class JobInformationModel {

    public jobTitle: string;
    public applicationType: string;
    public appliedDate: string;
    public jobStatus: string;
}

export class QuestionAnswerModel {
    /**
     *
     */
    constructor(jobHRKpiList?: QuestionAnswer[]) {

        this.jobHRKpiList = new Array<QuestionAnswer>();
    }

    public screeningQuestionList: QuestionAnswer[];
    public skillKpiList: QuestionAnswer[];
    public jobHRKpiList: QuestionAnswer[];
}

export class QuestionAnswer {

    constructor(jobQuestionId?: string, question?: string, weightage?: string, obtainedWeightage?: string) {

        this.jobHRQuestionId = jobQuestionId;
        this.question = question;
        this.weightage = weightage;
        this.obtainedWeightage = obtainedWeightage;
        
    }
    public id: string;
    public jobHRQuestionId: string;
    public question: string;
    public weightage: string;
    public obtainedWeightage: string;
    public jobApplicationId:string;
}

export class InterviewScheduleViewModel {
    constructor(interviewScheduleLevelList?: InterviewScheduleModel[]) {

        this.interviewScheduleLevelList = new Array<InterviewScheduleLevel>();
    }
    public interviewScheduleLevelList:InterviewScheduleLevel[];
    public managerList: DropDownList[];
    public interviewTypeList: DropDownList[];
    public interviewMode: DropDownList[];
}

export class InterviewScheduleLevel{
    constructor(interviewScheduleModelList?: InterviewScheduleModel[]) {

        this.interviewScheduleModelList = new Array<InterviewScheduleModel>();
    }
    public level:string;
  public levelId: string;
  public interviewId: string;
  public isLevelCompleted: boolean;
  public isInterviewCompleted: boolean;
  public levelManagerIds: string[]
    public interviewScheduleModelList:InterviewScheduleModel[];
}
export class InterviewScheduleModel {
  constructor(date?: Date, time?: string, managerName?: string, interviewStatus?: string, isCandidateSelected?: boolean, managerId?:string
  ) {
    this.date = date;
    this.time = time;
    this.managerName = managerName;
    this.interviewStatus = interviewStatus;
    this.isCandidateSelected = isCandidateSelected;
    this.managerId = managerId;
  }
  public date: Date;
  public time: string;
  public managerName: string;
  public comment: string;
  public interviewStatus: string;
  public isCandidateSelected: boolean;
  public managerId: string;
}
