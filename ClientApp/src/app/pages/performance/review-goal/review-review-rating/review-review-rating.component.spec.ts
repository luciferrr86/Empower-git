import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewReviewRatingComponent } from './review-review-rating.component';

describe('ReviewReviewRatingComponent', () => {
  let component: ReviewReviewRatingComponent;
  let fixture: ComponentFixture<ReviewReviewRatingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewReviewRatingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewReviewRatingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
