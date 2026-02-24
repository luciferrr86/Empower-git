import { DropDownList } from "../../common/dropdown";

export class SetGoalModel {

    lstSetGoal:SetGoal[];
    public searchIndividualGroup:DropDownList[];
    public searchFunctionalGroup:DropDownList[];
    public searchLevel:DropDownList[];
    public isGoalReleased:boolean;
    public isManagerRealeased:boolean;
    public isPerformanceStarted:boolean;
    public isCEO :boolean;
    public searchFunGroupId:string;
    public searchLevelId:string;
    public searchIndiGroupId:string;
       
}

export class SetGoal{  

    public performanceGoalId:string;
    public selectedPerformanceGoal:DropDownList[];
    public firstQuadrantId:string;
    public secondQuadrantId:string;
    public selectedQuadrant:DropDownList[];
    public setIndividual:string[];
    public selectedIndividualManager:DropDownList[];
    public setFunctionalGroupId:string[];
    public selectedFunctionalGroup:DropDownList[];
    public levelId:string;
    public selectedLevel:DropDownList[];
    public goalMeasure:string;
    public performanceGoalMeasureId:string;

 }

 export class Goal
 {
     public goalId:string;
     public goalName:string;
 }

 export class ReleaseGoalMessage
 {
     public status:number;
     public lstEmpName:string[];
 }