import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingClassesComponent } from './training-classes.component';

describe('TrainingClassesComponent', () => {
  let component: TrainingClassesComponent;
  let fixture: ComponentFixture<TrainingClassesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrainingClassesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingClassesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
