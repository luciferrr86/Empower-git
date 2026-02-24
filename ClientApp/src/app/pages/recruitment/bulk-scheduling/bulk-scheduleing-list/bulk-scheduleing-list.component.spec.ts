import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkScheduleingListComponent } from './bulk-scheduleing-list.component';

describe('BulkScheduleingListComponent', () => {
  let component: BulkScheduleingListComponent;
  let fixture: ComponentFixture<BulkScheduleingListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BulkScheduleingListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BulkScheduleingListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
