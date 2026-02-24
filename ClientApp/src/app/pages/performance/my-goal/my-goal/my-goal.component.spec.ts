import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyGoalComponent } from './my-goal.component';

describe('MyGoalComponent', () => {
  let component: MyGoalComponent;
  let fixture: ComponentFixture<MyGoalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyGoalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyGoalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
