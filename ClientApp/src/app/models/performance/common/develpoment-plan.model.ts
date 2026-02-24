import { CheckSaveSubmit } from "./check-save-submit.model";

export class DevelpomentPlan { 
    constructor() {
        this.employeeCareerDevList=new Array<CareerDevelopment>();
        this.managerCareerDevList=new Array<CareerDevelopment>();
        this.checkSaveSubmit=new CheckSaveSubmit();
    }
    public instructionText:string;
   public employeeCareerDevList:CareerDevelopment[];
   public managerCareerDevList:CareerDevelopment[];
   public checkSaveSubmit:CheckSaveSubmit;
   
}


export class CareerDevelopment {
    public careerDevId:string;
    public skillText:string;
    public careerInterestText:string;
}
