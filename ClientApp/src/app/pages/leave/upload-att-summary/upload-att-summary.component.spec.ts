import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadAttSummaryComponent } from './upload-att-summary.component';

describe('UploadAttSummaryComponent', () => {
  let component: UploadAttSummaryComponent;
  let fixture: ComponentFixture<UploadAttSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadAttSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadAttSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
