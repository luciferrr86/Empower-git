import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfessionalDetailEditorComponent } from './professional-detail-editor.component';

describe('ProfessionalDetailEditorComponent', () => {
  let component: ProfessionalDetailEditorComponent;
  let fixture: ComponentFixture<ProfessionalDetailEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProfessionalDetailEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfessionalDetailEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
