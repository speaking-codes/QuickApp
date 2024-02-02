import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { error } from 'console';
import { CustomerDetail } from 'src/app/models/customer';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { DashboardServiceService } from 'src/app/services/dashboard-service.service';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-dash-title',
  templateUrl: './dash-title.component.html',
  styleUrls: ['./dash-title.component.scss']
})
export class DashTitleComponent implements OnInit{
  dashboradTitle: string;

  @Input()
  customerCode = "";

  constructor(private dashboardService: DashboardServiceService, private alertService: AlertService){ }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void{
    this.dashboardService.getDashboardTitle(this.customerCode)
        .subscribe({
          next: result => this.onDataLoadSuccessfull(result),
          error: error => this.onDataLoadFailed(error)
        });
  }

  onDataLoadSuccessfull(data: CustomerDetail) {
    this.dashboradTitle = data.fullName;
  }

  onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }
}
