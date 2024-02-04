import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsurancecoverageComponent } from './insurancecoverage.component';

describe('InsurancecoverageComponent', () => {
  let component: InsurancecoverageComponent;
  let fixture: ComponentFixture<InsurancecoverageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InsurancecoverageComponent]
    });
    fixture = TestBed.createComponent(InsurancecoverageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
