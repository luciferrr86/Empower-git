import { TestBed, inject } from '@angular/core/testing';

import { LeaveRulesService } from './leave-rules.service';

describe('LeaveRulesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LeaveRulesService]
    });
  });

  it('should be created', inject([LeaveRulesService], (service: LeaveRulesService) => {
    expect(service).toBeTruthy();
  }));
});
