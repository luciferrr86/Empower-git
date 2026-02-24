import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkSchedulingPalleteComponent } from './bulk-scheduling-pallete.component';

describe('BulkSchedulingPalleteComponent', () => {
  let component: BulkSchedulingPalleteComponent;
  let fixture: ComponentFixture<BulkSchedulingPalleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BulkSchedulingPalleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BulkSchedulingPalleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
