import { Injectable } from '@angular/core';
import { DashboardEndpointServiceService } from './dashboard-endpoint-service.service';
import { CustomerDetail, CustomerHeader } from '../models/customer';
import { InsuranceCoverageSummary } from '../models/insurance-coverage';

@Injectable({
  providedIn: 'root'
})
export class DashboardServiceService {

  constructor(private dashboardEndpoint: DashboardEndpointServiceService) { }

  getCustomerHeader(customerCode: string) {
    return this.dashboardEndpoint.getCustomerHeaderEndpoint<CustomerHeader>(customerCode);    
  }

  getCustomerDetail(customerCode: string) {
    return this.dashboardEndpoint.getCustomerDetailEndpoint<CustomerDetail>(customerCode);    
  }

  getDashboardInsuranceCoverageSummary(customerCode: string){
    return this.dashboardEndpoint.getaDshboardInsuranceCoverageSummaryEndpoint<InsuranceCoverageSummary>(customerCode);
  }

  getDashboardInsuranceCoverageRecommended(customerCode: string){
    return this.dashboardEndpoint.getDashboardInsuranceCoverageRecommendedEndpoint(customerCode);
  }

  getDashboardInsuranceCoverageTopSelling(elementNumber: number, insuranceCoverageCodes: string){
    return this.dashboardEndpoint.getDashboardInsuranceCoverageTopSellingEndpoint(elementNumber, insuranceCoverageCodes);
  }

  getDashboardInsuranceCoverageOther(insuranceCoverageCodes: string){
    return this.dashboardEndpoint.getDashboardInsuranceCoverageOtherEndpoint(insuranceCoverageCodes);
  }
}
