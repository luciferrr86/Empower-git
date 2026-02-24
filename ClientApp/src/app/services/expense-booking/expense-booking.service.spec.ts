import { TestBed, inject } from '@angular/core/testing';

import { ExpenseBookingService } from './expense-booking.service';

describe('ExpenseBookingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ExpenseBookingService]
    });
  });

  it('should be created', inject([ExpenseBookingService], (service: ExpenseBookingService) => {
    expect(service).toBeTruthy();
  }));
});
