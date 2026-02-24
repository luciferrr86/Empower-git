import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TimesheetClientInfoComponent } from './timesheet-client-info.component';

describe('TimesheetClientInfoComponent', () => {
  let component: TimesheetClientInfoComponent;
  let fixture: ComponentFixture<TimesheetClientInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TimesheetClientInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TimesheetClientInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
