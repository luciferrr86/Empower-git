import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'rating-editor',
  templateUrl: './rating-editor.component.html',
  styleUrls: ['./rating-editor.component.css']
})
export class RatingEditorComponent implements OnInit {
  constructor() { }
  @Output() getRatingId: EventEmitter<string> = new EventEmitter<string>();
  @Input('groups')
  public ratingOptionForm: FormGroup;
  @Input('performanceStart')
  public isPerformanceStart: boolean;
  ngOnInit() {

  }

  removeRating(id: string) {
    this.getRatingId.emit(id);
  }

}
