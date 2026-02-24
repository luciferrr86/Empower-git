import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'company-contact-editor',
  templateUrl: './company-contact-editor.component.html',
  styleUrls: ['./company-contact-editor.component.css']
})
export class CompanyContactEditorComponent implements OnInit {

  @Input('group')
  public contactForm: FormGroup;

  @Output() getContactId: EventEmitter<string> = new EventEmitter<string>();
  
  constructor() { }

  ngOnInit() {
  }

  removeContactDetail(id: string) {
    this.getContactId.emit(id);
  }
}
