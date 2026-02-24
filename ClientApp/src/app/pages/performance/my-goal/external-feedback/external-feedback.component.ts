import { Component, OnInit, ViewChild } from '@angular/core';
import { ExternalFeedback } from '../../../../models/performance/my-goal/external-feedback.model';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'external-feedback',
  templateUrl: './external-feedback.component.html',
  styleUrls: ['./external-feedback.component.css']
})
export class ExternalFeedbackComponent implements OnInit {

  feedback: ExternalFeedback = new ExternalFeedback();

  @ViewChild('editorModal')
  editorModal: ModalDirective;

  constructor() { }

  ngOnInit() {
  }
  addFeddback() {
    this.feedback= new ExternalFeedback();
    this.editorModal.show();
    return this.feedback;
  }
}
