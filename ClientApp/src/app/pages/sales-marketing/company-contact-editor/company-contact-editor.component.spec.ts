import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyContactEditorComponent } from './company-contact-editor.component';

describe('CompanyContactEditorComponent', () => {
  let component: CompanyContactEditorComponent;
  let fixture: ComponentFixture<CompanyContactEditorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanyContactEditorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanyContactEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
