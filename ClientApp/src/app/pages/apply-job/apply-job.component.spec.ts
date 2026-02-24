import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEmpCtcComponent } from './apply-job.component';

describe('AddEmpCtcComponent', () => {
  let component: AddEmpCtcComponent;
  let fixture: ComponentFixture<AddEmpCtcComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddEmpCtcComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddEmpCtcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
