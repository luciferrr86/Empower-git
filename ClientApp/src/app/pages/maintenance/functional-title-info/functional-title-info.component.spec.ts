import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalTitleInfoComponent } from './functional-title-info.component';

describe('FunctionalTitleInfoComponent', () => {
  let component: FunctionalTitleInfoComponent;
  let fixture: ComponentFixture<FunctionalTitleInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalTitleInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalTitleInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
