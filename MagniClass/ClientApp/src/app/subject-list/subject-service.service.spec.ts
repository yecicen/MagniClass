import { TestBed } from '@angular/core/testing';

import { SubjectServiceService } from './subject-service.service';

describe('SubjectServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SubjectServiceService = TestBed.get(SubjectServiceService);
    expect(service).toBeTruthy();
  });
});
