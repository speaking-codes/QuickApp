import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DashboardHeaderComponent } from '../dashboard-header/dashboard-header.component';
import { DashTitleComponent } from '../dash-title/dash-title.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit{
    customerCode = null;
    constructor(private readonly route: ActivatedRoute){}

    ngOnInit(): void {
      this.customerCode = this.route.snapshot.params['customerCode'];      
    }
}
