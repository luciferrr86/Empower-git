import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { IOption } from '../../../../../../node_modules/ng-select';

@Component({
  selector: 'skill-interview-editor',
  templateUrl: './skill-interview-editor.component.html',
  styleUrls: ['./skill-interview-editor.component.css']
})
export class SkillInterviewEditorComponent {

  @Output() skillId: EventEmitter<string> = new EventEmitter<string>();
  @Input('group')
  public screeningQuestionForm: FormGroup;
  @Input('level')
  public levelList: Array<IOption>;

  removeSkillQuestion(id: string) {
    this.skillId.emit(id);
  }
}
