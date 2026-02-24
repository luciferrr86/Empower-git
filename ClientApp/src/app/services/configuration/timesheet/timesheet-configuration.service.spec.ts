import { TestBed, inject } from '@angular/core/testing';

import { TimesheetConfigurationService } from './timesheet-configuration.service';

describe('TimesheetConfigurationService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TimesheetConfigurationService]
    });
  });

  it('should be created', inject([TimesheetConfigurationService], (service: TimesheetConfigurationService) => {
    expect(service).toBeTruthy();
  }));
});
