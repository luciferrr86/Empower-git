import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TitleAmountComponent } from './title-amount.component';

describe('TitleAmountComponent', () => {
  let component: TitleAmountComponent;
  let fixture: ComponentFixture<TitleAmountComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TitleAmountComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TitleAmountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
