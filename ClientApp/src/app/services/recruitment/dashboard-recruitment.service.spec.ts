import { TestBed, inject } from '@angular/core/testing';

import { DashboardRecruitmentService } from './dashboard-recruitment.service';

describe('DashboardRecruitmentService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DashboardRecruitmentService]
    });
  });

  it('should be created', inject([DashboardRecruitmentService], (service: DashboardRecruitmentService) => {
    expect(service).toBeTruthy();
  }));
});
