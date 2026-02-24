import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidateChangePasswordComponent } from './candidate-change-password.component';

describe('CandidateChangePasswordComponent', () => {
  let component: CandidateChangePasswordComponent;
  let fixture: ComponentFixture<CandidateChangePasswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CandidateChangePasswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidateChangePasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
