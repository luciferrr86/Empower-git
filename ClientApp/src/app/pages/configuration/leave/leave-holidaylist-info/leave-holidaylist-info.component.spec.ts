import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveHolidaylistInfoComponent } from './leave-holidaylist-info.component';

describe('LeaveHolidaylistInfoComponent', () => {
  let component: LeaveHolidaylistInfoComponent;
  let fixture: ComponentFixture<LeaveHolidaylistInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveHolidaylistInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveHolidaylistInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
