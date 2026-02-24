import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DevelopmentPlanEditorComponent } from './development-plan-editor.component';

describe('DevelopmentPlanEditorComponent', () => {
  let component: DevelopmentPlanEditorComponent;
  let fixture: ComponentFixture<DevelopmentPlanEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DevelopmentPlanEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DevelopmentPlanEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
