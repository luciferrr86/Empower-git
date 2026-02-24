import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidatePersonalDetailComponent } from './candidate-personal-detail.component';

describe('CandidatePersonalDetailComponent', () => {
  let component: CandidatePersonalDetailComponent;
  let fixture: ComponentFixture<CandidatePersonalDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CandidatePersonalDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidatePersonalDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
