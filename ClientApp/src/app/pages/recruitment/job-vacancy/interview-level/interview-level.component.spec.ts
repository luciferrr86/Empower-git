import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewLevelComponent } from './interview-level.component';

describe('InterviewLevelComponent', () => {
  let component: InterviewLevelComponent;
  let fixture: ComponentFixture<InterviewLevelComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewLevelComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewLevelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
