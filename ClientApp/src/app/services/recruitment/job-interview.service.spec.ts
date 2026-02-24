import { TestBed, inject } from '@angular/core/testing';

import { JobInterviewService } from './job-interview.service';

describe('JobInterviewService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JobInterviewService]
    });
  });

  it('should be created', inject([JobInterviewService], (service: JobInterviewService) => {
    expect(service).toBeTruthy();
  }));
});
