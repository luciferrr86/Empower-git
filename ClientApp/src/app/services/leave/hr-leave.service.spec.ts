import { TestBed, inject } from '@angular/core/testing';

import { HrLeaveService } from './hr-leave.service';

describe('HrLeaveService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HrLeaveService]
    });
  });

  it('should be created', inject([HrLeaveService], (service: HrLeaveService) => {
    expect(service).toBeTruthy();
  }));
});
