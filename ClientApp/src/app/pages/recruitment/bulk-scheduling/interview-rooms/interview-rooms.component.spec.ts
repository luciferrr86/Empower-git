import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InterviewRoomsComponent } from './interview-rooms.component';

describe('InterviewRoomsComponent', () => {
  let component: InterviewRoomsComponent;
  let fixture: ComponentFixture<InterviewRoomsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterviewRoomsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterviewRoomsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
