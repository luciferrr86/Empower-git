import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewEmployeeTimesheetComponent } from './review-employee-timesheet.component';

describe('ReviewEmployeeTimesheetComponent', () => {
  let component: ReviewEmployeeTimesheetComponent;
  let fixture: ComponentFixture<ReviewEmployeeTimesheetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewEmployeeTimesheetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewEmployeeTimesheetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
