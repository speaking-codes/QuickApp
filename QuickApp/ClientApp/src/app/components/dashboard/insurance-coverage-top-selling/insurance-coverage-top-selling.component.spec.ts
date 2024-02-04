import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsuranceCoverageTopSellingComponent } from './insurance-coverage-top-selling.component';

describe('InsuranceCoverageTopSellingComponent', () => {
  let component: InsuranceCoverageTopSellingComponent;
  let fixture: ComponentFixture<InsuranceCoverageTopSellingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InsuranceCoverageTopSellingComponent]
    });
    fixture = TestBed.createComponent(InsuranceCoverageTopSellingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
