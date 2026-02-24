import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveApplliedDetailComponent } from './leave-appllied-detail.component';

describe('LeaveApplliedDetailComponent', () => {
  let component: LeaveApplliedDetailComponent;
  let fixture: ComponentFixture<LeaveApplliedDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveApplliedDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveApplliedDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
