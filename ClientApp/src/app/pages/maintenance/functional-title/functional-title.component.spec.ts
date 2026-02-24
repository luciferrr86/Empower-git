import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalTitleComponent } from './functional-title.component';

describe('FunctionalTitleComponent', () => {
  let component: FunctionalTitleComponent;
  let fixture: ComponentFixture<FunctionalTitleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalTitleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalTitleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
