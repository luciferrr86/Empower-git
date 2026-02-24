import { CheckSaveSubmit } from "./check-save-submit.model";

export class TrainingClassesModel {
    constructor() {      
        this.checkSaveSubmit=new CheckSaveSubmit();
        this.lstTrainingClasses=new Array<TrainingClasses>();
    }
    public instructionText:string;
    lstTrainingClasses:TrainingClasses[];
    checkSaveSubmit:CheckSaveSubmit;
}

export class TrainingClasses {
    public trainingClassId:string;
    public trainingClass:string;
}

