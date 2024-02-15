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
export class DashTitleComponent {
  @Input()
  customerFullName: string;

  constructor(){ }
}
