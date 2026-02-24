import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HrAssessmentComponent } from './hr-assessment.component';

describe('HrAssessmentComponent', () => {
  let component: HrAssessmentComponent;
  let fixture: ComponentFixture<HrAssessmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HrAssessmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HrAssessmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
