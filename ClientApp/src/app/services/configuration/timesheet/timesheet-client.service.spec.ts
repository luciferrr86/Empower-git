import { TestBed, inject } from '@angular/core/testing';

import { TimesheetClientService } from './timesheet-client.service';

describe('TimesheetClientService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TimesheetClientService]
    });
  });

  it('should be created', inject([TimesheetClientService], (service: TimesheetClientService) => {
    expect(service).toBeTruthy();
  }));
});
