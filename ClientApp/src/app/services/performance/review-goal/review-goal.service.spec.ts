import { TestBed, inject } from '@angular/core/testing';

import { ReviewGoalService } from './review-goal.service';

describe('ReviewGoalService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReviewGoalService]
    });
  });

  it('should be created', inject([ReviewGoalService], (service: ReviewGoalService) => {
    expect(service).toBeTruthy();
  }));
});
