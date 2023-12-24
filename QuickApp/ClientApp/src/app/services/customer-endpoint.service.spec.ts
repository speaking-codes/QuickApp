import { TestBed } from '@angular/core/testing';

import { CustomerEndpointService } from './customer-endpoint.service';

describe('CustomerEndpointService', () => {
  let service: CustomerEndpointService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CustomerEndpointService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
