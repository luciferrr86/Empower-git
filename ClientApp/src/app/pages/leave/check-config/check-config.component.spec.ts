import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CheckConfigComponent } from './check-config.component';

describe('CheckConfigComponent', () => {
  let component: CheckConfigComponent;
  let fixture: ComponentFixture<CheckConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CheckConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
