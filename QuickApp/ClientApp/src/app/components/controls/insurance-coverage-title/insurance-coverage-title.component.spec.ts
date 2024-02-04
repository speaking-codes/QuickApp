import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsuranceCoverageTitleComponent } from './insurance-coverage-title.component';

describe('InsuranceCoverageTitleComponent', () => {
  let component: InsuranceCoverageTitleComponent;
  let fixture: ComponentFixture<InsuranceCoverageTitleComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InsuranceCoverageTitleComponent]
    });
    fixture = TestBed.createComponent(InsuranceCoverageTitleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
