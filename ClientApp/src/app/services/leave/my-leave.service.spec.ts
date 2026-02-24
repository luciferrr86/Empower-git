import { TestBed, inject } from '@angular/core/testing';

import { MyLeaveService } from './my-leave.service';

describe('MyLeaveService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MyLeaveService]
    });
  });

  it('should be created', inject([MyLeaveService], (service: MyLeaveService) => {
    expect(service).toBeTruthy();
  }));
});
