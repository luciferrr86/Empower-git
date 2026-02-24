import { TestBed, inject } from '@angular/core/testing';

import { CreateExpenseItemService } from './create-expense-item.service';

describe('CreateExpenseItemService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CreateExpenseItemService]
    });
  });

  it('should be created', inject([CreateExpenseItemService], (service: CreateExpenseItemService) => {
    expect(service).toBeTruthy();
  }));
});
