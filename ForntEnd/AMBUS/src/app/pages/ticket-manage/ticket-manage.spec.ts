import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketManage } from './ticket-manage';

describe('TicketManage', () => {
  let component: TicketManage;
  let fixture: ComponentFixture<TicketManage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketManage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketManage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
