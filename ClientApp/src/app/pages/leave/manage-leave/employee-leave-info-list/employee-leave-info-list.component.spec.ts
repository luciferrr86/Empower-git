import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeLeaveInfoListComponent } from './employee-leave-info-list.component';

describe('EmployeeLeaveInfoListComponent', () => {
  let component: EmployeeLeaveInfoListComponent;
  let fixture: ComponentFixture<EmployeeLeaveInfoListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeLeaveInfoListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeLeaveInfoListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
