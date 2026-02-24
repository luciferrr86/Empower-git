import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PerformanceGoalComponent } from './performance-goal.component';

describe('PerformanceGoalComponent', () => {
  let component: PerformanceGoalComponent;
  let fixture: ComponentFixture<PerformanceGoalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PerformanceGoalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PerformanceGoalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
