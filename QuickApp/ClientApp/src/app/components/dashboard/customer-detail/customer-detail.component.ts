import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { error } from 'console';
import { CustomerDetail } from 'src/app/models/customer';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { DashboardServiceService } from 'src/app/services/dashboard-service.service';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss']
})
export class CustomerDetailComponent implements OnInit{
  @Input()
  customerCode = "";
  
  @Input()
  showDetail: boolean;

  customerDetail: CustomerDetail;

  constructor(private dashboardService: DashboardServiceService, private alertService: AlertService) { }

  ngOnInit(): void {
     this.loadData();
  }

  loadData(): void{
    this.dashboardService.getCustomerDetail(this.customerCode)
        .subscribe({
          next: result => this.onDataLoadSuccessfull(result),
          error: error => this.onDataLoadFailed(error)
        });
  }

  onDataLoadSuccessfull(data: CustomerDetail) {
    this.alertService.stopLoadingMessage();
    this.customerDetail = data;
  }

  onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }

  isShowCustomerDetail(): boolean{
    return this.showDetail;
  }
}
