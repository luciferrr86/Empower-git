import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JobTypeInfoComponent } from './job-type-info.component';

describe('JobTypeInfoComponent', () => {
  let component: JobTypeInfoComponent;
  let fixture: ComponentFixture<JobTypeInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JobTypeInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JobTypeInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
