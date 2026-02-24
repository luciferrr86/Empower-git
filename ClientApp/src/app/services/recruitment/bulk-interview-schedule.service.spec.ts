import { TestBed, inject } from '@angular/core/testing';

import { BulkInterviewScheduleService } from './bulk-interview-schedule.service';

describe('BulkInterviewScheduleService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BulkInterviewScheduleService]
    });
  });

  it('should be created', inject([BulkInterviewScheduleService], (service: BulkInterviewScheduleService) => {
    expect(service).toBeTruthy();
  }));
});
