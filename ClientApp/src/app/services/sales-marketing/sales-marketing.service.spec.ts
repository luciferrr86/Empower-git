import { TestBed, inject } from '@angular/core/testing';

import { SalesMarketingService } from './sales-marketing.service';

describe('SalesMarketingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SalesMarketingService]
    });
  });

  it('should be created', inject([SalesMarketingService], (service: SalesMarketingService) => {
    expect(service).toBeTruthy();
  }));
});
