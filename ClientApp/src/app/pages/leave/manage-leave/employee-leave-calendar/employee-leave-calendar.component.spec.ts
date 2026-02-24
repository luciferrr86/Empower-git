import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeLeaveCalendarComponent } from './employee-leave-calendar.component';

describe('EmployeeLeaveCalendarComponent', () => {
  let component: EmployeeLeaveCalendarComponent;
  let fixture: ComponentFixture<EmployeeLeaveCalendarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeLeaveCalendarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeLeaveCalendarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
