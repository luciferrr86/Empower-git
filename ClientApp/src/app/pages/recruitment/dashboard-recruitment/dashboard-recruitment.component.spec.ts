import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardRecruitmentComponent } from './dashboard-recruitment.component';

describe('DashboardRecruitmentComponent', () => {
  let component: DashboardRecruitmentComponent;
  let fixture: ComponentFixture<DashboardRecruitmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DashboardRecruitmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardRecruitmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
