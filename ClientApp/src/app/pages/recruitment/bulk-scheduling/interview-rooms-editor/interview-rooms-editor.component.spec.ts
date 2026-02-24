import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewRoomsEditorComponent } from './interview-rooms-editor.component';

describe('InterviewRoomsEditorComponent', () => {
  let component: InterviewRoomsEditorComponent;
  let fixture: ComponentFixture<InterviewRoomsEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewRoomsEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewRoomsEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
