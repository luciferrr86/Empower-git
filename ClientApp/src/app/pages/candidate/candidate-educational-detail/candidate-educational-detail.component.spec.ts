import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidateEducationalDetailComponent } from './candidate-educational-detail.component';

describe('CandidateEducationalDetailComponent', () => {
  let component: CandidateEducationalDetailComponent;
  let fixture: ComponentFixture<CandidateEducationalDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CandidateEducationalDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidateEducationalDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
