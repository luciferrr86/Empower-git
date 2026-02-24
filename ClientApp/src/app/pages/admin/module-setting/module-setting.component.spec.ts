import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleSettingComponent } from './module-setting.component';

describe('ModuleSettingComponent', () => {
  let component: ModuleSettingComponent;
  let fixture: ComponentFixture<ModuleSettingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ModuleSettingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ModuleSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
