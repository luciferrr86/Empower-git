import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAttSummaryComponent } from './view-att-summary.component';

describe('ViewAttSummaryComponent', () => {
  let component: ViewAttSummaryComponent;
  let fixture: ComponentFixture<ViewAttSummaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewAttSummaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewAttSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
