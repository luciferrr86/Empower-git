import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalDesignationComponent } from './functional-designation.component';

describe('FunctionalDesignationComponent', () => {
  let component: FunctionalDesignationComponent;
  let fixture: ComponentFixture<FunctionalDesignationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalDesignationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalDesignationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
