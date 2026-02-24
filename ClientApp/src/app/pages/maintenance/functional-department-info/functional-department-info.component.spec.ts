import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalDepartmentInfoComponent } from './functional-department-info.component';

describe('FunctionalDepartmentInfoComponent', () => {
  let component: FunctionalDepartmentInfoComponent;
  let fixture: ComponentFixture<FunctionalDepartmentInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalDepartmentInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalDepartmentInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
