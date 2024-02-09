import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsuranceCoverageFooterComponent } from './insurance-coverage-footer.component';

describe('InsuranceCoverageFooterComponent', () => {
  let component: InsuranceCoverageFooterComponent;
  let fixture: ComponentFixture<InsuranceCoverageFooterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InsuranceCoverageFooterComponent]
    });
    fixture = TestBed.createComponent(InsuranceCoverageFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
