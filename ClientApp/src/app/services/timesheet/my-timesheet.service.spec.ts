import { TestBed, inject } from '@angular/core/testing';

import { MyTimesheetService } from './my-timesheet.service';

describe('MyTimesheetService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MyTimesheetService]
    });
  });

  it('should be created', inject([MyTimesheetService], (service: MyTimesheetService) => {
    expect(service).toBeTruthy();
  }));
});
