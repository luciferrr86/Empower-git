import { TestBed, inject } from '@angular/core/testing';

import { EndpointFactory } from './endpoint-factory.service';

describe('EndpointFactory', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EndpointFactory]
    });
  });

  it('should be created', inject([EndpointFactory], (service: EndpointFactory) => {
    expect(service).toBeTruthy();
  }));
});
