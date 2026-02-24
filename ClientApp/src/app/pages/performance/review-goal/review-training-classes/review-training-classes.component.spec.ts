import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewTrainingClassesComponent } from './review-training-classes.component';

describe('ReviewTrainingClassesComponent', () => {
  let component: ReviewTrainingClassesComponent;
  let fixture: ComponentFixture<ReviewTrainingClassesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewTrainingClassesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewTrainingClassesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
