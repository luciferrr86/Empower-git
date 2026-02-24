import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckSalaryComponent } from './check-salary.component';

describe('CheckSalaryComponent', () => {
  let component: CheckSalaryComponent;
  let fixture: ComponentFixture<CheckSalaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CheckSalaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckSalaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
