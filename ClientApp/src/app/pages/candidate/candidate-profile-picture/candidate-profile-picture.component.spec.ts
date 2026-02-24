import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidateProfilePictureComponent } from './candidate-profile-picture.component';

describe('CandidateProfilePictureComponent', () => {
  let component: CandidateProfilePictureComponent;
  let fixture: ComponentFixture<CandidateProfilePictureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CandidateProfilePictureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidateProfilePictureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
