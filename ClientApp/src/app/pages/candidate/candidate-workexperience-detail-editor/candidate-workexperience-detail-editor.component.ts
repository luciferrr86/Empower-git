import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'candidate-workexperience-detail-editor',
  templateUrl: './candidate-workexperience-detail-editor.component.html',
  styleUrls: ['./candidate-workexperience-detail-editor.component.css']
})
export class CandidateWorkexperienceDetailEditorComponent implements OnInit {
  @Input('group')
  public professionalForm: FormGroup;
  constructor() { }

  ngOnInit() {
  }

}
