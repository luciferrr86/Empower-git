import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DevelopmentPlanComponent } from './development-plan.component';

describe('DevelopmentPlanComponent', () => {
  let component: DevelopmentPlanComponent;
  let fixture: ComponentFixture<DevelopmentPlanComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DevelopmentPlanComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DevelopmentPlanComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
