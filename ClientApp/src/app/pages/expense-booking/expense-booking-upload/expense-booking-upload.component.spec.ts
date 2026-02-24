import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseBookingUploadComponent } from './expense-booking-upload.component';

describe('ExpenseBookingUploadComponent', () => {
  let component: ExpenseBookingUploadComponent;
  let fixture: ComponentFixture<ExpenseBookingUploadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExpenseBookingUploadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpenseBookingUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
