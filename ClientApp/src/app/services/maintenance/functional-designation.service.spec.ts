import { TestBed, inject } from '@angular/core/testing';

import { FunctionalDesignationService } from './functional-designation.service';

describe('FunctionalDesignationService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FunctionalDesignationService]
    });
  });

  it('should be created', inject([FunctionalDesignationService], (service: FunctionalDesignationService) => {
    expect(service).toBeTruthy();
  }));
});
