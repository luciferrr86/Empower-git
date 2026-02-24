import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomaticScheduleComponent } from './automatic-schedule.component';

describe('AutomaticScheduleComponent', () => {
  let component: AutomaticScheduleComponent;
  let fixture: ComponentFixture<AutomaticScheduleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutomaticScheduleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutomaticScheduleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
