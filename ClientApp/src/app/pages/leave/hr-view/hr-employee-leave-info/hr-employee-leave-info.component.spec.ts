import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HrEmployeeLeaveInfoComponent } from './hr-employee-leave-info.component';

describe('HrEmployeeLeaveInfoComponent', () => {
  let component: HrEmployeeLeaveInfoComponent;
  let fixture: ComponentFixture<HrEmployeeLeaveInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HrEmployeeLeaveInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HrEmployeeLeaveInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
