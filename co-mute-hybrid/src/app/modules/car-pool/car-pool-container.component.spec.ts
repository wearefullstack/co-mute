import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarPoolContainerComponent } from './car-pool-container.component';

describe('CarPoolContainerComponent', () => {
  let component: CarPoolContainerComponent;
  let fixture: ComponentFixture<CarPoolContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarPoolContainerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarPoolContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
