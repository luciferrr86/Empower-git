import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveWorkingdayComponent } from './leave-workingday.component';

describe('LeaveWorkingdayComponent', () => {
  let component: LeaveWorkingdayComponent;
  let fixture: ComponentFixture<LeaveWorkingdayComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveWorkingdayComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveWorkingdayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
