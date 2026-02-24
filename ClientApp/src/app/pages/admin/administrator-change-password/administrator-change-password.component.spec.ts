import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministratorChangePasswordComponent } from './administrator-change-password.component';

describe('AdministratorChangePasswordComponent', () => {
  let component: AdministratorChangePasswordComponent;
  let fixture: ComponentFixture<AdministratorChangePasswordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdministratorChangePasswordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdministratorChangePasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
