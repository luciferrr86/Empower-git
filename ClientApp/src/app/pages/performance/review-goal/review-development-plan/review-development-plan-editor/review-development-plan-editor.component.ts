import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'review-development-plan-editor',
  templateUrl: './review-development-plan-editor.component.html',
  styleUrls: ['./review-development-plan-editor.component.css']
})
export class ReviewDevelopmentPlanEditorComponent implements OnInit {
  @Input('devPlanList')
  public devPlanForm: FormGroup;

  @Input('isEmployeeEditor')
  public isEmployeeEditor: boolean;



  @Input('isSubmit')
  public isEmpDevGoalSubmit: boolean;


  constructor() { }

  ngOnInit() {
    if (this.isEmpDevGoalSubmit) {
      this.devPlanForm.disable();
    }
    else if (this.isEmployeeEditor) {
      this.devPlanForm.disable();
    }

  }


}
