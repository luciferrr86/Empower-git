import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewTypeInfoComponent } from './interview-type-info.component';

describe('InterviewTypeInfoComponent', () => {
  let component: InterviewTypeInfoComponent;
  let fixture: ComponentFixture<InterviewTypeInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewTypeInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewTypeInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
