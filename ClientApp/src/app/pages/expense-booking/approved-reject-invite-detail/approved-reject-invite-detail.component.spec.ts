import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprovedRejectInviteDetailComponent } from './approved-reject-invite-detail.component';

describe('ApprovedRejectInviteDetailComponent', () => {
  let component: ApprovedRejectInviteDetailComponent;
  let fixture: ComponentFixture<ApprovedRejectInviteDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApprovedRejectInviteDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApprovedRejectInviteDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
