import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalGroupInfoComponent } from './functional-group-info.component';

describe('FunctionalGroupInfoComponent', () => {
  let component: FunctionalGroupInfoComponent;
  let fixture: ComponentFixture<FunctionalGroupInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalGroupInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalGroupInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
