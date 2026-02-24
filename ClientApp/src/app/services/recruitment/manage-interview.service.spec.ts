import { TestBed, inject } from '@angular/core/testing';

import { ManageInterviewService } from './manage-interview.service';

describe('ManageInterviewService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ManageInterviewService]
    });
  });

  it('should be created', inject([ManageInterviewService], (service: ManageInterviewService) => {
    expect(service).toBeTruthy();
  }));
});
