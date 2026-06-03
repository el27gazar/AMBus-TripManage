import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RouteManage } from './route-manage';

describe('RouteManage', () => {
  let component: RouteManage;
  let fixture: ComponentFixture<RouteManage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouteManage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RouteManage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
