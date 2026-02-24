import { TestBed, inject } from '@angular/core/testing';

import { MyGoalService } from './my-goal.service';

describe('MyGoalService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MyGoalService]
    });
  });

  it('should be created', inject([MyGoalService], (service: MyGoalService) => {
    expect(service).toBeTruthy();
  }));
});
