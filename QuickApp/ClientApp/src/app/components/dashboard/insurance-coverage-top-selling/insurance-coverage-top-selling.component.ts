import { Component, OnInit } from '@angular/core';
import { InsuranceCoverageTitleComponent } from '../../controls/insurance-coverage-title/insurance-coverage-title.component';

@Component({
  selector: 'app-insurance-coverage-top-selling',
  templateUrl: './insurance-coverage-top-selling.component.html',
  styleUrls: ['./insurance-coverage-top-selling.component.scss']
})
export class InsuranceCoverageTopSellingComponent implements OnInit {
  titleDescription: string = "Beni più Acquistati";

  ngOnInit(): void {
    
  }
}
