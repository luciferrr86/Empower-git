import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { IOption } from '../../../../../../node_modules/ng-select';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'interview-level-editor',
  templateUrl: './interview-level-editor.component.html',
  styleUrls: ['./interview-level-editor.component.css']
})
export class InterviewLevelEditorComponent implements OnInit {
  @Output() getInterviewLevelId: EventEmitter<string> = new EventEmitter<string>();
  constructor() { }

  ngOnInit() {
   
  }
  @Input('group')
  public interviewLevelForm: FormGroup;

  @Input('hiringManagerList')
  public hiringManagerList: Array<IOption>;

  removeInterviewLevel(id: string) {
    this.getInterviewLevelId.emit(id);
  }
}
