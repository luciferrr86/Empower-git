import { TestBed, inject } from '@angular/core/testing';

import { SetGoalService } from './set-goal.service';

describe('SetGoalService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SetGoalService]
    });
  });

  it('should be created', inject([SetGoalService], (service: SetGoalService) => {
    expect(service).toBeTruthy();
  }));
});
