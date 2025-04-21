import { TestBed } from '@angular/core/testing';

import { ServiesapiService } from './serviesapi.service';

describe('ServiesapiService', () => {
  let service: ServiesapiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiesapiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
