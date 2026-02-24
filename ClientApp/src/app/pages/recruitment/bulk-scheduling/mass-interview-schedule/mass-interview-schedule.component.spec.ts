import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MassInterviewScheduleComponent } from './mass-interview-schedule.component';

describe('MassInterviewScheduleComponent', () => {
  let component: MassInterviewScheduleComponent;
  let fixture: ComponentFixture<MassInterviewScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MassInterviewScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MassInterviewScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
