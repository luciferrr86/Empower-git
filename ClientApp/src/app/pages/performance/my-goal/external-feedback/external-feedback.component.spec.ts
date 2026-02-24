import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExternalFeedbackComponent } from './external-feedback.component';

describe('ExternalFeedbackComponent', () => {
  let component: ExternalFeedbackComponent;
  let fixture: ComponentFixture<ExternalFeedbackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExternalFeedbackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExternalFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
