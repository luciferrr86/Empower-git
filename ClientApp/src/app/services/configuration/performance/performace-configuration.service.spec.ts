import { TestBed, inject } from '@angular/core/testing';

import { PerformaceConfigurationService } from './performace-configuration.service';

describe('PerformaceConfigurationService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PerformaceConfigurationService]
    });
  });

  it('should be created', inject([PerformaceConfigurationService], (service: PerformaceConfigurationService) => {
    expect(service).toBeTruthy();
  }));
});
