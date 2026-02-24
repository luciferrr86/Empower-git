import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetAssignProjectComponent } from './timesheet-assign-project.component';

describe('TimesheetAssignProjectComponent', () => {
  let component: TimesheetAssignProjectComponent;
  let fixture: ComponentFixture<TimesheetAssignProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetAssignProjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetAssignProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
