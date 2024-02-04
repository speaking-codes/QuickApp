import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DashboardHeaderComponent } from '../dashboard-header/dashboard-header.component';
import { DashTitleComponent } from '../dash-title/dash-title.component';
import { InsurancecoverageComponent } from '../insurancecoverage/insurancecoverage.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit{
    customerCode: string = "";
    customerFullName: string = "";

    constructor(private readonly route: ActivatedRoute){}

    ngOnInit(): void {
      this.customerCode = this.route.snapshot.params['customerCode'];      
    }

    onLoad($event): void{
      this.customerFullName = $event;
    }
}
