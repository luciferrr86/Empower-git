import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalGroupComponent } from './functional-group.component';

describe('FunctionalGroupComponent', () => {
  let component: FunctionalGroupComponent;
  let fixture: ComponentFixture<FunctionalGroupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalGroupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
