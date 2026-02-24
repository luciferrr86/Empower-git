import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewTypeComponent } from './interview-type.component';

describe('InterviewTypeComponent', () => {
  let component: InterviewTypeComponent;
  let fixture: ComponentFixture<InterviewTypeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewTypeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
