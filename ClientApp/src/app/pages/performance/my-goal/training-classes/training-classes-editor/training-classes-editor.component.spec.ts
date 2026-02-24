import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingClassesEditorComponent } from './training-classes-editor.component';

describe('TrainingClassesEditorComponent', () => {
  let component: TrainingClassesEditorComponent;
  let fixture: ComponentFixture<TrainingClassesEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrainingClassesEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingClassesEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
