import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewDevelopmentPlanEditorComponent } from './review-development-plan-editor.component';

describe('ReviewDevelopmentPlanEditorComponent', () => {
  let component: ReviewDevelopmentPlanEditorComponent;
  let fixture: ComponentFixture<ReviewDevelopmentPlanEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewDevelopmentPlanEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewDevelopmentPlanEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
