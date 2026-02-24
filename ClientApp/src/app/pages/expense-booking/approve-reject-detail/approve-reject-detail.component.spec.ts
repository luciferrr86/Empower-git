import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveRejectDetailComponent } from './approve-reject-detail.component';

describe('ApproveRejectDetailComponent', () => {
  let component: ApproveRejectDetailComponent;
  let fixture: ComponentFixture<ApproveRejectDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveRejectDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveRejectDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
