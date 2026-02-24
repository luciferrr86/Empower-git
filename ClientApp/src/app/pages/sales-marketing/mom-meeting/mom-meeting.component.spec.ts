import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MomMeetingComponent } from './mom-meeting.component';

describe('MomMeetingComponent', () => {
  let component: MomMeetingComponent;
  let fixture: ComponentFixture<MomMeetingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MomMeetingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MomMeetingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
