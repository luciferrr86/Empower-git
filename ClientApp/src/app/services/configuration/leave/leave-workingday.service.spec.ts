import { TestBed, inject } from '@angular/core/testing';

import { LeaveWorkingdayService } from './leave-workingday.service';

describe('LeaveWorkingdayService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LeaveWorkingdayService]
    });
  });

  it('should be created', inject([LeaveWorkingdayService], (service: LeaveWorkingdayService) => {
    expect(service).toBeTruthy();
  }));
});
