import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewLevelEditorComponent } from './interview-level-editor.component';

describe('InterviewLevelEditorComponent', () => {
  let component: InterviewLevelEditorComponent;
  let fixture: ComponentFixture<InterviewLevelEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewLevelEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewLevelEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
