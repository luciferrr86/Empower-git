import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewDevelopmentPlanComponent } from './review-development-plan.component';

describe('ReviewDevelopmentPlanComponent', () => {
  let component: ReviewDevelopmentPlanComponent;
  let fixture: ComponentFixture<ReviewDevelopmentPlanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewDevelopmentPlanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewDevelopmentPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
