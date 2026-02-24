import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetTemplateComponent } from './timesheet-template.component';

describe('TimesheetTemplateComponent', () => {
  let component: TimesheetTemplateComponent;
  let fixture: ComponentFixture<TimesheetTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
