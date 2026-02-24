import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeSalaryDetailComponent } from './employee-salary-detail.component';

describe('EmployeeSalaryDetailComponent', () => {
  let component: EmployeeSalaryDetailComponent;
  let fixture: ComponentFixture<EmployeeSalaryDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeeSalaryDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeSalaryDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
