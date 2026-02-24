import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidateWorkexperienceDetailEditorComponent } from './candidate-workexperience-detail-editor.component';

describe('CandidateWorkexperienceDetailEditorComponent', () => {
  let component: CandidateWorkexperienceDetailEditorComponent;
  let fixture: ComponentFixture<CandidateWorkexperienceDetailEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CandidateWorkexperienceDetailEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidateWorkexperienceDetailEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
