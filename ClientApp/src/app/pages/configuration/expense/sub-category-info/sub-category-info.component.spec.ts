import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SubCategoryInfoComponent } from './sub-category-info.component';

describe('SubCategoryInfoComponent', () => {
  let component: SubCategoryInfoComponent;
  let fixture: ComponentFixture<SubCategoryInfoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SubCategoryInfoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SubCategoryInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
