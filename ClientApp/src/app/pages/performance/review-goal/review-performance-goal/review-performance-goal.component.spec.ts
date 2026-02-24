import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewPerformanceGoalComponent } from './review-performance-goal.component';

describe('ReviewPerformanceGoalComponent', () => {
  let component: ReviewPerformanceGoalComponent;
  let fixture: ComponentFixture<ReviewPerformanceGoalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewPerformanceGoalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewPerformanceGoalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
