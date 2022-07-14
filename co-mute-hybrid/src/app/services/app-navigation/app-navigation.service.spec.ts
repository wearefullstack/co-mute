import { TestBed } from '@angular/core/testing';

import { AppNavigationService } from './app-navigation.service';

describe('AppNavigationService', () => {
  let service: AppNavigationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppNavigationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
