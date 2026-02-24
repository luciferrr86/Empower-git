import { TestBed, inject } from '@angular/core/testing';

import { BulkUploadService } from './bulk-upload.service';

describe('BulkUploadService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BulkUploadService]
    });
  });

  it('should be created', inject([BulkUploadService], (service: BulkUploadService) => {
    expect(service).toBeTruthy();
  }));
});
