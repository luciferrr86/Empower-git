import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveHolidaylistComponent } from './leave-holidaylist.component';

describe('LeaveHolidaylistComponent', () => {
  let component: LeaveHolidaylistComponent;
  let fixture: ComponentFixture<LeaveHolidaylistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveHolidaylistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveHolidaylistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
