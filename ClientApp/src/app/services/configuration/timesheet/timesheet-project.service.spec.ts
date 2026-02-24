import { TestBed, inject } from '@angular/core/testing';

import { TimesheetProjectService } from './timesheet-project.service';

describe('TimesheetProjectService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TimesheetProjectService]
    });
  });

  it('should be created', inject([TimesheetProjectService], (service: TimesheetProjectService) => {
    expect(service).toBeTruthy();
  }));
});
