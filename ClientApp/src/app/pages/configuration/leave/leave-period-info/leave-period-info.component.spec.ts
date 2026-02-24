import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeavePeriodInfoComponent } from './leave-period-info.component';

describe('LeavePeriodInfoComponent', () => {
  let component: LeavePeriodInfoComponent;
  let fixture: ComponentFixture<LeavePeriodInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeavePeriodInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeavePeriodInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
