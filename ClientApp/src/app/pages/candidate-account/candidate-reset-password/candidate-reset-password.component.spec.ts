import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidateResetPasswordComponent } from './candidate-reset-password.component';

describe('CandidateResetPasswordComponent', () => {
  let component: CandidateResetPasswordComponent;
  let fixture: ComponentFixture<CandidateResetPasswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CandidateResetPasswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidateResetPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
