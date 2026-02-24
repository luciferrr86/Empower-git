import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'professional-detail-editor',
  templateUrl: './professional-detail-editor.component.html',
  styleUrls: ['./professional-detail-editor.component.css']
})
export class ProfessionalDetailEditorComponent implements OnInit {

  @Input('group')
  public professionalForm: FormGroup;
  constructor() { }

  ngOnInit() {

  }

}
