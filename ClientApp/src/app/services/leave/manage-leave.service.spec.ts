import { TestBed, inject } from '@angular/core/testing';

import { ManageLeaveService } from './manage-leave.service';

describe('ManageLeaveService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ManageLeaveService]
    });
  });

  it('should be created', inject([ManageLeaveService], (service: ManageLeaveService) => {
    expect(service).toBeTruthy();
  }));
});
