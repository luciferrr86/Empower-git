import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetEmployeeListComponent } from './timesheet-employee-list.component';

describe('TimesheetEmployeeListComponent', () => {
  let component: TimesheetEmployeeListComponent;
  let fixture: ComponentFixture<TimesheetEmployeeListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetEmployeeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetEmployeeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
