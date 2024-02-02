import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsuranceCoverageSummaryComponent } from './insurance-coverage-summary.component';

describe('InsuranceCoverageSummaryComponent', () => {
  let component: InsuranceCoverageSummaryComponent;
  let fixture: ComponentFixture<InsuranceCoverageSummaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InsuranceCoverageSummaryComponent]
    });
    fixture = TestBed.createComponent(InsuranceCoverageSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
