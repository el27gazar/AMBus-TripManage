import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TripManage } from './trip-manage';

describe('TripManage', () => {
  let component: TripManage;
  let fixture: ComponentFixture<TripManage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TripManage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TripManage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
