import { TestBed, inject } from '@angular/core/testing';

import { HrViewService } from './hr-view.service';

describe('HrViewService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HrViewService]
    });
  });

  it('should be created', inject([HrViewService], (service: HrViewService) => {
    expect(service).toBeTruthy();
  }));
});
