import { TestBed, inject } from '@angular/core/testing';

import { LeaveHolidayListService } from './leave-holiday-list.service';

describe('LeaveHolidayListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LeaveHolidayListService]
    });
  });

  it('should be created', inject([LeaveHolidayListService], (service: LeaveHolidayListService) => {
    expect(service).toBeTruthy();
  }));
});
