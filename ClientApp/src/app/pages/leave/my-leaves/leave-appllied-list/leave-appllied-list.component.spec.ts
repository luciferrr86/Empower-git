import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveApplliedListComponent } from './leave-appllied-list.component';

describe('LeaveApplliedListComponent', () => {
  let component: LeaveApplliedListComponent;
  let fixture: ComponentFixture<LeaveApplliedListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeaveApplliedListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeaveApplliedListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
