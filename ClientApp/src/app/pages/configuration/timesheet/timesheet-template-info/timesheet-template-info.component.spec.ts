import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetTemplateInfoComponent } from './timesheet-template-info.component';

describe('TimesheetTemplateInfoComponent', () => {
  let component: TimesheetTemplateInfoComponent;
  let fixture: ComponentFixture<TimesheetTemplateInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetTemplateInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetTemplateInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
