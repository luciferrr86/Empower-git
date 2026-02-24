import { TestBed, inject } from '@angular/core/testing';

import { JobVacancyFormDataService } from './job-vacancy-form-data.service';

describe('JobVacancyFormDataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [JobVacancyFormDataService]
    });
  });

  it('should be created', inject([JobVacancyFormDataService], (service: JobVacancyFormDataService) => {
    expect(service).toBeTruthy();
  }));
});
