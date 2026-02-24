import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetScheduleComponent } from './timesheet-schedule.component';

describe('TimesheetScheduleComponent', () => {
  let component: TimesheetScheduleComponent;
  let fixture: ComponentFixture<TimesheetScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
