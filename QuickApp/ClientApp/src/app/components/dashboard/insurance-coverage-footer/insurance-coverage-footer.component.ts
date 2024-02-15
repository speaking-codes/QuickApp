import { Component, Input, OnInit } from '@angular/core';
import { DivStyleDirective } from 'src/app/directives/div-style.directive';
import { IconStyleDirective } from 'src/app/directives/icon-style.directive';
import { InsuranceCoveragePolicyFooter } from 'src/app/models/insurance-coverage';

@Component({
  selector: 'app-insurance-coverage-footer',
  templateUrl: './insurance-coverage-footer.component.html',
  styleUrls: ['./insurance-coverage-footer.component.scss']
})
export class InsuranceCoverageFooterComponent implements OnInit {
  
  @Input()
  insuranceCoveragePolicies: InsuranceCoveragePolicyFooter[] = [];
  
  ngOnInit(): void {
    // this.insuranceCoveragePolicies = [];
  }

  IsShowButton(): boolean{
    return this.insuranceCoveragePolicies.length != 0;
  }
}
