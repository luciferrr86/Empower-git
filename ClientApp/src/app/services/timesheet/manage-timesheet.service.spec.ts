import { TestBed, inject } from '@angular/core/testing';

import { ManageTimesheetService } from './manage-timesheet.service';

describe('ManageTimesheetService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ManageTimesheetService]
    });
  });

  it('should be created', inject([ManageTimesheetService], (service: ManageTimesheetService) => {
    expect(service).toBeTruthy();
  }));
});
