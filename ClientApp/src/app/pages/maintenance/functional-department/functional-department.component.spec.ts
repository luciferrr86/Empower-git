import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalDepartmentComponent } from './functional-department.component';

describe('FunctionalDepartmentComponent', () => {
  let component: FunctionalDepartmentComponent;
  let fixture: ComponentFixture<FunctionalDepartmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalDepartmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
