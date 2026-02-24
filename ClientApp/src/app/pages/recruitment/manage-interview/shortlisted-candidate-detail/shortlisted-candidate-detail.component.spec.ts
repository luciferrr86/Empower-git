import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShortlistedCandidateDetailComponent } from './shortlisted-candidate-detail.component';

describe('ShortlistedCandidateDetailComponent', () => {
  let component: ShortlistedCandidateDetailComponent;
  let fixture: ComponentFixture<ShortlistedCandidateDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShortlistedCandidateDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShortlistedCandidateDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
