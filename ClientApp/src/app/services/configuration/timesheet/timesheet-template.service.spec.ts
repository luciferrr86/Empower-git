import { TestBed, inject } from '@angular/core/testing';

import { TimesheetTemplateService } from './timesheet-template.service';

describe('TimesheetTemplateService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TimesheetTemplateService]
    });
  });

  it('should be created', inject([TimesheetTemplateService], (service: TimesheetTemplateService) => {
    expect(service).toBeTruthy();
  }));
});
