import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';


@Component({
  selector: 'screening-editor',
  templateUrl: './screening-editor.component.html',
  styleUrls: ['./screening-editor.component.css']
})
export class ScreeningEditorComponent {

  @Output() screeningId: EventEmitter<string> = new EventEmitter<string>();
  @Input('group')
  public screeningQuestionForm: FormGroup;

  removeQuestion(id: string) {    
    this.screeningId.emit(id);
  }
}
