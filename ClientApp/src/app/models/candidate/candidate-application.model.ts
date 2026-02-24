export class ApplicationForm {
    constructor(jobType?: string, jobTitle?: string, jobLocation?: string, publishedDate?: string) {

        this.jobType = jobType;
        this.jobTitle = jobTitle;
        this.jobLocation = jobLocation;
        this.publishedDate = publishedDate;

    }

    public jobType: string;
    public jobTitle: string;
    public jobLocation: string;
    public publishedDate: string;
    public questionListModel: JobScreeningQuestion[];

}

export class JobScreeningQuestion {
    constructor(id?: string, question?: string, weightage?: string, mandatory?: string, controlType?: string,
        option1?: string, option2?: string, option3?: string, option4?: string,
        OptChk1?: string, OptChk2?: string, OptChk3?: string, OptChk4?: string) {

        this.id = id;
        this.question = question;
        this.weightage = weightage;
        this.mandatory = mandatory;
        this.controlType = controlType;
        this.option1 = option1;
        this.option2 = option2;
        this.option3 = option3;
        this.option4 = option4;
        this.OptChk1 = OptChk1;
        this.OptChk2 = OptChk2;
        this.OptChk3 = OptChk3;
        this.OptChk4 = OptChk4;
    }
    public id: string;
    public question: string;
    public weightage: string;
    public mandatory: string;
    public controlType: string;
    public option1: string;
    public option2: string;
    public option3: string;
    public option4: string;
    public OptChk1: string;
    public OptChk2: string;
    public OptChk3: string;
    public OptChk4: string;

}