import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidateWorkexperienceDetailComponent } from './candidate-workexperience-detail.component';

describe('CandidateWorkexperienceDetailComponent', () => {
  let component: CandidateWorkexperienceDetailComponent;
  let fixture: ComponentFixture<CandidateWorkexperienceDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CandidateWorkexperienceDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidateWorkexperienceDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
