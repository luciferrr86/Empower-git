import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HrKpiEditorComponent } from './hr-kpi-editor.component';

describe('HrKpiEditorComponent', () => {
  let component: HrKpiEditorComponent;
  let fixture: ComponentFixture<HrKpiEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HrKpiEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HrKpiEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
