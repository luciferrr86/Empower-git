import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewEmpDetailComponent } from './review-emp-detail.component';

describe('ReviewEmpDetailComponent', () => {
  let component: ReviewEmpDetailComponent;
  let fixture: ComponentFixture<ReviewEmpDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReviewEmpDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewEmpDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
