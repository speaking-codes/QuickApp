import { Component, OnInit } from '@angular/core';
import { InsuranceCoverageTitleComponent } from '../../controls/insurance-coverage-title/insurance-coverage-title.component';

@Component({
  selector: 'app-insurance-coverage-other',
  templateUrl: './insurance-coverage-other.component.html',
  styleUrls: ['./insurance-coverage-other.component.scss']
})
export class InsuranceCoverageOtherComponent implements OnInit{
  titleDescription: string = "Altri Beni";

  ngOnInit(): void {
    
  }
}
