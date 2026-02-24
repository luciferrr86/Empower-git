import { CheckSaveSubmit } from "./check-save-submit.model";

export class GoalViewModel{   
    constructor() {      
        this.checkSaveSubmit=new CheckSaveSubmit();
    }
    public empGoalId:string;
    public midYearGoalMeasureList:GoalMeasure[];
    public endYearGoalMeasureList:GoalMeasure[];
    public checkSaveSubmit:CheckSaveSubmit;
}

export class GoalMeasure{
    public goalId:string;
    public goalName:string;
    public measure:string;   
    public accomplishment:string;
    
    public managerComments:string;
    public startTime:string;
    public endTime:string;
}
