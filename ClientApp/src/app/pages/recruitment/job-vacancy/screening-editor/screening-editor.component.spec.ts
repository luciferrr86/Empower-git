import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScreeningEditorComponent } from './screening-editor.component';

describe('ScreeningEditorComponent', () => {
  let component: ScreeningEditorComponent;
  let fixture: ComponentFixture<ScreeningEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScreeningEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScreeningEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
