import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagerAssesmentComponent } from './manager-assesment.component';

describe('ManagerAssesmentComponent', () => {
  let component: ManagerAssesmentComponent;
  let fixture: ComponentFixture<ManagerAssesmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManagerAssesmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManagerAssesmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
