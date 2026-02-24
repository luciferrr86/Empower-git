import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetClientComponent } from './timesheet-client.component';

describe('TimesheetClientComponent', () => {
  let component: TimesheetClientComponent;
  let fixture: ComponentFixture<TimesheetClientComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetClientComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
