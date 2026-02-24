import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { JobInterviewPaletteComponent } from './job-interview-palette.component';

describe('JobInterviewPaletteComponent', () => {
  let component: JobInterviewPaletteComponent;
  let fixture: ComponentFixture<JobInterviewPaletteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ JobInterviewPaletteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(JobInterviewPaletteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
