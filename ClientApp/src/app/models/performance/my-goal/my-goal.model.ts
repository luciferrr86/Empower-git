import { DropDownList } from "../../common/dropdown";
import { CheckSaveSubmit } from "../common/check-save-submit.model";

export class MyGoal {
    /**
     *
     */
    constructor() {
       this.reviewedDetail=new EmployeeDetail();
        
    }
    public employeeId:string;
    public  goalCurrentYearViewModel:GoalViewModel;   
    public checkGoal:CheckSaveSubmit;
    public reviewedDetail: EmployeeDetail;    
    public  historyId :string;
    public  roleAccessList:DropDownList[];
    public empPerformanceHistory:DropDownList[];
    public  goalText :string;
}
export class EmployeeDetail{
    public name:string;
    public functionalDepartment:string;
    public evaluatorName:string;
    public dateReview:string;
    public functionalGroup:string;
    public title:string;     
}

export class GoalViewModel{
    public employeeId:string;
    public empGoalId:string;
    public isManager:boolean;
    public midYearGoalMeasureList:GoalMeasure[];
    public endYearGoalMeasureList:GoalMeasure[];
    public viewGoalMeasureList:GoalMeasure[];
    public completedByConfig:string;
    public checkGoal:CheckSaveSubmit;
    public empPerformanceHistory:DropDownList[];
    
    public isMidYearEnabled :boolean;
    public isMidYearRatingSubmitted:boolean;
    public isEndYearRatingSubmitted:boolean;
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
