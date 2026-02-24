import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetScheduleInfoComponent } from './timesheet-schedule-info.component';

describe('TimesheetScheduleInfoComponent', () => {
  let component: TimesheetScheduleInfoComponent;
  let fixture: ComponentFixture<TimesheetScheduleInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetScheduleInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetScheduleInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
