import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeesRequestComponent } from './employees-request.component';

describe('EmployeesRequestComponent', () => {
  let component: EmployeesRequestComponent;
  let fixture: ComponentFixture<EmployeesRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EmployeesRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeesRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
