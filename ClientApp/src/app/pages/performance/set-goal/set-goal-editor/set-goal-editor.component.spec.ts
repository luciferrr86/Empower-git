import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SetGoalEditorComponent } from './set-goal-editor.component';

describe('SeGoalEditorComponent', () => {
  let component: SetGoalEditorComponent;
  let fixture: ComponentFixture<SetGoalEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SetGoalEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetGoalEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
