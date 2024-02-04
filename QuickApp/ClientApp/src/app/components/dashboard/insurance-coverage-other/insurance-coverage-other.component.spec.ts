import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsuranceCoverageOtherComponent } from './insurance-coverage-other.component';

describe('InsuranceCoverageOtherComponent', () => {
  let component: InsuranceCoverageOtherComponent;
  let fixture: ComponentFixture<InsuranceCoverageOtherComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InsuranceCoverageOtherComponent]
    });
    fixture = TestBed.createComponent(InsuranceCoverageOtherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
