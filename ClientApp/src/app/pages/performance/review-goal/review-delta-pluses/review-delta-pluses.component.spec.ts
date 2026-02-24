import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewDeltaPlusesComponent } from './review-delta-pluses.component';

describe('ReviewDeltaPlusesComponent', () => {
  let component: ReviewDeltaPlusesComponent;
  let fixture: ComponentFixture<ReviewDeltaPlusesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewDeltaPlusesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewDeltaPlusesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
