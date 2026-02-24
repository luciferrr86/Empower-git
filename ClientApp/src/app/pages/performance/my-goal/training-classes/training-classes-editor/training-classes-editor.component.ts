import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '../../../../../../../node_modules/@angular/forms';


@Component({
  selector: 'training-classes-editor',
  templateUrl: './training-classes-editor.component.html',
  styleUrls: ['./training-classes-editor.component.css']
})
export class TrainingClassesEditorComponent implements OnInit {

  @Input('traininglist')
  public trainingClassesForm: FormGroup;

  @Input('isSubmit')
  public isSubmit: boolean;

  @Output() getTrainingClassesId: EventEmitter<string> = new EventEmitter<string>();


  constructor() { }

  ngOnInit() {

    if (this.isSubmit) {
      this.trainingClassesForm.disable();
    }
  }

  removeTrainingClasses(id: string) {
    this.getTrainingClassesId.emit(id);
  }

}
