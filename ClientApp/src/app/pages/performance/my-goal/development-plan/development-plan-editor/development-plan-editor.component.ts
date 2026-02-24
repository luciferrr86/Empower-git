import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'development-plan-editor',
  templateUrl: './development-plan-editor.component.html',
  styleUrls: ['./development-plan-editor.component.css']
})
export class DevelopmentPlanEditorComponent implements OnInit {
  @Input('devPlanList')
  public devPlanForm: FormGroup;

  @Input('isManagerEditor')
  public isManagerEditor: boolean;



  @Input('isSubmit')
  public isEmpDevGoalSubmit: boolean;


  constructor() { }

  ngOnInit() {
    if (this.isEmpDevGoalSubmit) {
      this.devPlanForm.disable();
    }
    else if (this.isManagerEditor) {
      this.devPlanForm.disable();
    }

  }

}
