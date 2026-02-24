import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeavePeriodComponent } from './leave-period.component';

describe('LeavePeriodComponent', () => {
  let component: LeavePeriodComponent;
  let fixture: ComponentFixture<LeavePeriodComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeavePeriodComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeavePeriodComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
