import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'hr-kpi-editor',
  templateUrl: './hr-kpi-editor.component.html',
  styleUrls: ['./hr-kpi-editor.component.css']
})
export class HrKpiEditorComponent  {

  @Output() hrQuestionId:EventEmitter<string>=new EventEmitter<string>();
  @Input('group')
  public hrQuestionForm: FormGroup;

  removeHrKpi(id:string)
  {
    this.hrQuestionId.emit(id);
  }

}
