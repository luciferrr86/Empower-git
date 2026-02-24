import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HrViewComponent } from './hr-view.component';

describe('HrViewComponent', () => {
  let component: HrViewComponent;
  let fixture: ComponentFixture<HrViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HrViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HrViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
