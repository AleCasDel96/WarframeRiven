import { TestBed } from '@angular/core/testing';

import { WarframeAlertService } from './warframe-alert.service';

describe('WarframeAlertService', () => {
  let service: WarframeAlertService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WarframeAlertService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
