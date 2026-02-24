import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseBookingRequestComponent } from './expense-booking-request.component';

describe('ExpenseBookingRequestComponent', () => {
  let component: ExpenseBookingRequestComponent;
  let fixture: ComponentFixture<ExpenseBookingRequestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExpenseBookingRequestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpenseBookingRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
