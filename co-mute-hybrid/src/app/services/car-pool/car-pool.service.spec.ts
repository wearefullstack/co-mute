import { TestBed } from '@angular/core/testing';

import { CarPoolService } from './car-pool.service';

describe('CarPoolService', () => {
  let service: CarPoolService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CarPoolService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
