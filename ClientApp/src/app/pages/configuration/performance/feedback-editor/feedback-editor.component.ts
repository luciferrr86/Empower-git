import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'feedback-editor',
  templateUrl: './feedback-editor.component.html',
  styleUrls: ['./feedback-editor.component.css']
})
export class FeedbackEditorComponent implements OnInit {

  @Output() getFeedbackId: EventEmitter<string> = new EventEmitter<string>();
  @Input('group')
  public feedbackOptionForm: FormGroup;

  @Input('performanceStart')
  public isPerformanceStart: boolean;

  ngOnInit() {

  }


  removeFeedback(id: string) {
    this.getFeedbackId.emit(id);
  }


}
