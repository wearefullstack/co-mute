import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyCarPoolsComponent } from './my-car-pools.component';

describe('MyCarPoolsComponent', () => {
  let component: MyCarPoolsComponent;
  let fixture: ComponentFixture<MyCarPoolsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyCarPoolsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyCarPoolsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
