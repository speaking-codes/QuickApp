import { Component, OnInit } from '@angular/core';
import { DivStyleDirective } from 'src/app/directives/div-style.directive';
import { IconStyleDirective } from 'src/app/directives/icon-style.directive';
import { InsuranceCoveragePolicyFooter } from 'src/app/models/insurance-coverage';

@Component({
  selector: 'app-insurance-coverage-footer',
  templateUrl: './insurance-coverage-footer.component.html',
  styleUrls: ['./insurance-coverage-footer.component.scss']
})
export class InsuranceCoverageFooterComponent implements OnInit {
  insuranceCoveragePolicies: InsuranceCoveragePolicyFooter[] = [];

  backColor: string = "#CC0066";
  backStyleCss: string = "health-insurance-coverage-btn";

  ngOnInit(): void {
    this.insuranceCoveragePolicies = [
      new InsuranceCoveragePolicyFooter("A01", "Auto", "fa-ship", "travel-insurance-coverage-btn"),
      new InsuranceCoveragePolicyFooter("A02", "Moto", "fa-suitcase-rolling", "health-insurance-coverage-btn"),
      new InsuranceCoveragePolicyFooter("A01", "Auto", "fa-ship", "health-insurance-coverage-btn"),
      new InsuranceCoveragePolicyFooter("A02", "Moto", "fa-stethoscope", "travel-insurance-coverage-btn"),
      new InsuranceCoveragePolicyFooter("A01", "Auto", "fa-ship", "health-insurance-coverage-btn")
    ];
    debugger;
  }
}
