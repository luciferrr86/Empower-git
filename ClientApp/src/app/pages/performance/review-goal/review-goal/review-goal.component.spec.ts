import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewGoalComponent } from './review-goal.component';

describe('ReviewGoalComponent', () => {
  let component: ReviewGoalComponent;
  let fixture: ComponentFixture<ReviewGoalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewGoalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewGoalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
