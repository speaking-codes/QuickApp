import { Component, OnInit } from '@angular/core';
import { InsuranceCoverageTitleComponent } from '../../controls/insurance-coverage-title/insurance-coverage-title.component';

@Component({
  selector: 'app-insurance-coverage-recommender',
  templateUrl: './insurance-coverage-recommender.component.html',
  styleUrls: ['./insurance-coverage-recommender.component.scss']
})
export class InsuranceCoverageRecommenderComponent implements OnInit {
  titleDescription: string = "Beni Suggeriti al Cliente";

  ngOnInit(): void {
    
  }
}
