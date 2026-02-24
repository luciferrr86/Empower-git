import { TestBed, inject } from '@angular/core/testing';

import { LeavePeriodService } from './leave-period.service';

describe('LeavePeriodService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LeavePeriodService]
    });
  });

  it('should be created', inject([LeavePeriodService], (service: LeavePeriodService) => {
    expect(service).toBeTruthy();
  }));
});
