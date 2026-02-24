import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AllEmpSalComponent } from './all-emp-sal.component';

describe('AllEmpSalComponent', () => {
  let component: AllEmpSalComponent;
  let fixture: ComponentFixture<AllEmpSalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AllEmpSalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AllEmpSalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
