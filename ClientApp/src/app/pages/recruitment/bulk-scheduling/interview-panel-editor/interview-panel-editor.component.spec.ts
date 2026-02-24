import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewPanelEditorComponent } from './interview-panel-editor.component';

describe('InterviewPanelEditorComponent', () => {
  let component: InterviewPanelEditorComponent;
  let fixture: ComponentFixture<InterviewPanelEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewPanelEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewPanelEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
