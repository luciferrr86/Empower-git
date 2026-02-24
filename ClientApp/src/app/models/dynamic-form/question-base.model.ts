export class QuestionBase<T>{
  value: T;
  key:string;
  text:string;
  required:boolean;
  order:number;
  controlType:string;
}
export class DropDownQuestion extends QuestionBase<string>{
  options = [];
  controlType = 'dropdown';
  constructor(){
      super();
  }
}
export class TextboxQuestion extends QuestionBase<string>{
  type:string;
  controlType = 'textbox';
  constructor(){
      super();
  }
}
export class CheckBoxQuestion extends QuestionBase<string>{
  type:string;
  options = [];
  controlType = 'checkbox';
  constructor(){
      super();
  }
}
export class RadioButtonQuestion extends QuestionBase<string>{
  type:string;
  options = [];
  controlType = 'radio';
  constructor(){
      super();
  }
}
export class ApplicationQuestion{
  constructor( value?: string[],){
    this.option=new Array<string>();
  }
  public id:string;
  public option:string[];
}
export class ApplicationQuestionModel{
  questions = new Array<ApplicationQuestion>();
  candidateId:string;
  jobVacancyId:string;

}