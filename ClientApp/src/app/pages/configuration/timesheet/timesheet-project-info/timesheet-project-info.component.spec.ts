import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetProjectInfoComponent } from './timesheet-project-info.component';

describe('TimesheetProjectInfoComponent', () => {
  let component: TimesheetProjectInfoComponent;
  let fixture: ComponentFixture<TimesheetProjectInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetProjectInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetProjectInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
