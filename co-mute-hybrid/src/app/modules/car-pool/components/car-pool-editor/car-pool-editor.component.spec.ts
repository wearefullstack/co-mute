import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarPoolEditorComponent } from './car-pool-editor.component';

describe('CarPoolEditorComponent', () => {
  let component: CarPoolEditorComponent;
  let fixture: ComponentFixture<CarPoolEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarPoolEditorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarPoolEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
