import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerGridComponent } from './customer-grid.component';

describe('CustomerGridComponent', () => {
  let component: CustomerGridComponent;
  let fixture: ComponentFixture<CustomerGridComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerGridComponent]
    });
    fixture = TestBed.createComponent(CustomerGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
