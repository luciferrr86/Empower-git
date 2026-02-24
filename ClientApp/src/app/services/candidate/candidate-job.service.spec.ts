import { TestBed, inject } from '@angular/core/testing';

import { CandidateJobService } from './candidate-job.service';

describe('CandidateJobService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CandidateJobService]
    });
  });

  it('should be created', inject([CandidateJobService], (service: CandidateJobService) => {
    expect(service).toBeTruthy();
  }));
});
