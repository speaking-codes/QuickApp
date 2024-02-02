import { Component, Input, OnInit } from '@angular/core';
import { InsuranceCoverageSummary } from 'src/app/models/insurance-coverage';

@Component({
  selector: 'app-insurance-coverage-summary',
  templateUrl: './insurance-coverage-summary.component.html',
  styleUrls: ['./insurance-coverage-summary.component.scss']
})
export class InsuranceCoverageSummaryComponent implements OnInit {
  @Input()
  customerCode = "";
  
  @Input()
  showDetail: boolean;
  
  insuranceCoverageList: InsuranceCoverageSummary[];

  ngOnInit(): void {
      this.loadData();
  }

  loadData() {
    this.insuranceCoverageList = [
      new InsuranceCoverageSummary ("V001-VC001-00000001", "Auto", "Mazda 2 2° Serie, 1.3 86 CV", "12/05/2023", "12/05/2024", "€ 1350,00"),
      new InsuranceCoverageSummary ("V001-VC001-00000002", "Attività Lavorativa", "Mazda 2 2° Serie, 1.3 86 CV", "12/05/2023", "12/05/2024", "€ 1350,00"),
      new InsuranceCoverageSummary ("V001-VC001-00000003", "Animali Domestici", "Mazda 2 2° Serie, 1.3 86 CV", "12/05/2023", "12/05/2024", "€ 1350,00"),
      new InsuranceCoverageSummary ("V001-VC001-00000004", "Grandi Interventi", "Mazda 2 2° Serie, 1.3 86 CV", "12/05/2023", "12/05/2024", "€ 1350,00"),
      new InsuranceCoverageSummary ("V001-VC001-00000005", "Bagagli", "Mazda 2 2° Serie, 1.3 86 CV", "12/05/2023", "12/05/2024", "€ 1350,00")
    ];
  }

  isShowInsuranceCoverage(): boolean{
    return this.showDetail;
  }
}
