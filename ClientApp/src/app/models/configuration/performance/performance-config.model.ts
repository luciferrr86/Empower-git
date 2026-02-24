import { Feedback } from "./feedback.model";
import { Rating } from "./rating.model";


export class PerformanceConfig {
   public id:string;
   public isPerformanceStart:boolean;
    myGoalInstructionText:string;
    careerDevInstructionText:string;
    trainingClassesInstructionText:string;
    performanceConfigFeebackViewModel:Feedback[];
    performanceConfigRatingViewModel:Rating[];
}
