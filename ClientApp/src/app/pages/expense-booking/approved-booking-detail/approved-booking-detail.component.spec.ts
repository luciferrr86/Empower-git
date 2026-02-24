import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprovedBookingDetailComponent } from './approved-booking-detail.component';

describe('ApprovedBookingDetailComponent', () => {
  let component: ApprovedBookingDetailComponent;
  let fixture: ComponentFixture<ApprovedBookingDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApprovedBookingDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApprovedBookingDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
