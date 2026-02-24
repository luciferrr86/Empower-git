import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveTypeInfoComponent } from './leave-type-info.component';

describe('LeaveTypeInfoComponent', () => {
  let component: LeaveTypeInfoComponent;
  let fixture: ComponentFixture<LeaveTypeInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveTypeInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveTypeInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
