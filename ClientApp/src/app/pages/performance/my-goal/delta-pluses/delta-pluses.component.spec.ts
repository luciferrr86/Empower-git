import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeltaPlusesComponent } from './delta-pluses.component';

describe('DeltaPlusesComponent', () => {
  let component: DeltaPlusesComponent;
  let fixture: ComponentFixture<DeltaPlusesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeltaPlusesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeltaPlusesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
