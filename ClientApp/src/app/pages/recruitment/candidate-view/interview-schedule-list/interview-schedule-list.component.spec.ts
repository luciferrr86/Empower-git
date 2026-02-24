import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewScheduleListComponent } from './interview-schedule-list.component';

describe('InterviewScheduleListComponent', () => {
  let component: InterviewScheduleListComponent;
  let fixture: ComponentFixture<InterviewScheduleListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewScheduleListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewScheduleListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
