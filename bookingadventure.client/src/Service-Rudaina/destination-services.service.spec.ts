import { TestBed } from '@angular/core/testing';

import { DestinationServicesService } from './destination-services.service';

describe('DestinationServicesService', () => {
  let service: DestinationServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DestinationServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
