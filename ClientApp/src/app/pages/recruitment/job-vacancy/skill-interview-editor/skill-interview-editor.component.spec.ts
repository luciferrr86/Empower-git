import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SkillInterviewEditorComponent } from './skill-interview-editor.component';

describe('SkillInterviewEditorComponent', () => {
  let component: SkillInterviewEditorComponent;
  let fixture: ComponentFixture<SkillInterviewEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SkillInterviewEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SkillInterviewEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
