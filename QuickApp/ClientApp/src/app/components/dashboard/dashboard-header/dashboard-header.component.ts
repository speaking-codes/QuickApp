import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CustomerHeaderComponent } from '../customer-header/customer-header.component';
import { ChartComponent } from '../chart/chart.component';
import { CustomerDetail } from 'src/app/models/customer';

@Component({
  selector: 'app-dashboard-header',
  templateUrl: './dashboard-header.component.html',
  styleUrls: ['./dashboard-header.component.scss']
})
export class DashboardHeaderComponent implements OnInit {
  @Input()
  customerCode = "";

  @Output()
  loaded =  new EventEmitter<string>();  
  
  showCustomerDetail: boolean;
  showInsuranceCoverage: boolean;
  showChartLeggend: boolean;
  heightChart: number;
 
  ngOnInit(): void {
    this.showCustomerDetail = false;
    this.showInsuranceCoverage = false;
    this.showChartLeggend = false;
    this.heightChart = 200;
  }

  IsShowenCustomerDetail(): boolean {
    return this.showCustomerDetail;
  }

  ShowCustomerDetail(): void {
    this.showCustomerDetail = true;
    this.showChartLeggend = false;
    this.heightChart = 600;
  }

  HideCustomerDetail(): void {
    this.showCustomerDetail = false;
    this.showChartLeggend = false;
    this.heightChart = 200;
  }

  IsShowenInsuranceCoverage(): boolean {
    return this.showInsuranceCoverage;
  }

  ShowInsuranceCoverage(): void {
    this.showInsuranceCoverage = true;    
  } 
  
  HideInsuranceCoverage(): void {
    this.showInsuranceCoverage = false;
  }

  onLoad(customerName: string){
    debugger;
    this.loaded.emit(customerName);
  }
}
