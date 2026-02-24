import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthCandidateComponent } from './auth-candidate.component';

describe('AuthCandidateComponent', () => {
  let component: AuthCandidateComponent;
  let fixture: ComponentFixture<AuthCandidateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthCandidateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthCandidateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
