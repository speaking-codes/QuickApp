import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { TableColumn } from '@swimlane/ngx-datatable';
import { InsuranceCoverageSummary } from 'src/app/models/insurance-coverage';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { DashboardServiceService } from 'src/app/services/dashboard-service.service';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-insurance-coverage-summary',
  templateUrl: './insurance-coverage-summary.component.html',
  styleUrls: ['./insurance-coverage-summary.component.scss']
})
export class InsuranceCoverageSummaryComponent implements OnInit {
  columns: TableColumn[] = [];
  rows: InsuranceCoverageSummary[] = [];
  loadingIndicator = false;  
  
  @Input()
  customerCode = "";
  
  @Input()
  showDetail: boolean;
  
  @Input()
  verticalScrollbar = false;
  
  @Input()
  pageSize = 5;

  insuranceCoverageList: InsuranceCoverageSummary[];

  constructor(private dashboardService: DashboardServiceService, private alertService: AlertService) { }
  
  ngOnInit(): void {
      this.loadData();
  }

  loadData() {
    this.loadingIndicator = true;    
    
    this.dashboardService.getDashboardInsuranceCoverageSummary(this.customerCode)
          .subscribe({
              next: results => this.onDataLoadSuccessfull(results),
              error: error => this.onDataLoadFailed(error)
      });   

    setTimeout(() => { this.loadingIndicator = false; }, 1500);    
  }

  onDataLoadSuccessfull(data: InsuranceCoverageSummary[]): void {
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;
    this.refreshDataIndexes(data);
    this.rows = [...data];    
  }

  onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }

  refreshDataIndexes(data: InsuranceCoverageSummary[]) {
    let index = 0;

    for (const i of data) {
      i.$$index = index++;
    }
  }

  isShowInsuranceCoverage(): boolean{
    return this.showDetail;
  }
}
