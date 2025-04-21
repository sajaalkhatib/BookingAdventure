import { TestBed } from '@angular/core/testing';

import { RudainaAdminService } from './rudaina-admin.service';

describe('RudainaAdminService', () => {
  let service: RudainaAdminService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RudainaAdminService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
