import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsuranceCoverageRecommenderComponent } from './insurance-coverage-recommender.component';

describe('InsuranceCoverageRecommenderComponent', () => {
  let component: InsuranceCoverageRecommenderComponent;
  let fixture: ComponentFixture<InsuranceCoverageRecommenderComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InsuranceCoverageRecommenderComponent]
    });
    fixture = TestBed.createComponent(InsuranceCoverageRecommenderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
