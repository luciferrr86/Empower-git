import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewDateTimeComponent } from './interview-date-time.component';

describe('InterviewDateTimeComponent', () => {
  let component: InterviewDateTimeComponent;
  let fixture: ComponentFixture<InterviewDateTimeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewDateTimeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewDateTimeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
