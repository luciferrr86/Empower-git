import { DropDownList } from "../../common/dropdown";

export interface SkillInterview {
    levelList:DropDownList[];
    jobSkillQuestion: QuestionOption[];
}

export interface QuestionOption {
    question: string;
    weightage:number;
}