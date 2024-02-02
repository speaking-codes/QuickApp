import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { error } from 'console';
import { CustomerHeader } from 'src/app/models/customer';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { DashboardServiceService } from 'src/app/services/dashboard-service.service';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-customer-header',
  templateUrl: './customer-header.component.html',
  styleUrls: ['./customer-header.component.scss']
})
export class CustomerHeaderComponent implements OnInit {
  customerHeader: CustomerHeader;

  @Input()
  customerCode = "";

  constructor(private dashboardService: DashboardServiceService, private alertService: AlertService) { }

  ngOnInit(): void {
      this.loadData();
  }

  loadData(): void { 
    this.dashboardService.getCustomerHeader(this.customerCode)
        .subscribe({
            next: results => this.onDataLoadSuccessfull(results),
            error: error => this.onDataLoadFailed(error)
        });
  }

  onDataLoadSuccessfull(data: CustomerHeader) {
    this.alertService.stopLoadingMessage();
    this.customerHeader = data;
  }

  onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }
}
