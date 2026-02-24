import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalDesignationInfoComponent } from './functional-designation-info.component';

describe('FunctionalDesignationInfoComponent', () => {
  let component: FunctionalDesignationInfoComponent;
  let fixture: ComponentFixture<FunctionalDesignationInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalDesignationInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalDesignationInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
