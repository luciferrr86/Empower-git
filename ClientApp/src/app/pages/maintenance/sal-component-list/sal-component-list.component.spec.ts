import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SalComponentListComponent } from './sal-component-list.component';

describe('SalComponentListComponent', () => {
  let component: SalComponentListComponent;
  let fixture: ComponentFixture<SalComponentListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SalComponentListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SalComponentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
