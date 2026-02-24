import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveRulesInfoComponent } from './leave-rules-info.component';

describe('LeaveRulesInfoComponent', () => {
  let component: LeaveRulesInfoComponent;
  let fixture: ComponentFixture<LeaveRulesInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveRulesInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveRulesInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
