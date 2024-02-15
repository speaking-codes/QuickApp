import { Injectable } from '@angular/core';
import { ConfigurationService } from './configuration.service';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { EndpointBase } from './endpoint-base.service';
import { Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardEndpointServiceService extends EndpointBase {

    get dashboardCustomerHeaderUrl() { return this.configurations.baseUrl + '/api/Dashboard/CustomerHeader/';}
    get dashboardCustomerDetailUrl() { return this.configurations.baseUrl + '/api/Dashboard/CustomerDetail/';}
    get dashboardInsuranceCoverageSummaryUrl() { return this.configurations.baseUrl + '/api/Dashboard/InsuranceCoverageSummary/'; }
    get dashboardInsuranceCoverageRecommendedUrl() { return this.configurations.baseUrl + '/api/Dashboard/InsuranceCoverageRecommended/'}
    get dashboardInsuranceCoverageTopSellingUrl() { return this.configurations.baseUrl + '/api/Dashboard/InsuranceCoverageTopSelling/'; }
    get dashboardInsuranceCoverageOtherUrl() { return this.configurations.baseUrl + '/api/Dashboard/InsuranceCoverageOther'; }
    get dashboardInsuranceCoverageChartUrl() { return this.configurations.baseUrl + '/api/Dashboard/InsuranceCoverageChart/'; }

    constructor(private configurations: ConfigurationService, http: HttpClient, authService: AuthService) {  super(http, authService); }

    getCustomerHeaderEndpoint<T>(customerCode: string): Observable<T> {
      const endpointUrl = `${this.dashboardCustomerHeaderUrl}${customerCode}`;
  
      return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
        catchError(error => {
          return this.handleError(error, () => this.getCustomerHeaderEndpoint<T>(customerCode));
        }));
    }

    getCustomerDetailEndpoint<T>(customerCode: string): Observable<T> {
      const endpointUrl = `${this.dashboardCustomerDetailUrl}${customerCode}`;
  
      return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
        catchError(error => {
          return this.handleError(error, () => this.getCustomerDetailEndpoint<T>(customerCode));
        }));
    }

    getaDshboardInsuranceCoverageSummaryEndpoint<T>(customerCode: string){
      const endpointUrl = `${this.dashboardInsuranceCoverageSummaryUrl}${customerCode}`;
      
      return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
        catchError(error => {
          return this.handleError(error, () => this.getaDshboardInsuranceCoverageSummaryEndpoint<T>(customerCode));
        }));
    }

    getDashboardInsuranceCoverageRecommendedEndpoint<T>(customerCode: string) { 
      const endpointUrl = `${this.dashboardInsuranceCoverageRecommendedUrl}${customerCode}`;
      
      return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
       catchError(error => {
         return this.handleError(error, () => this.getDashboardInsuranceCoverageRecommendedEndpoint<T>(customerCode));
       }));
    }

    getDashboardInsuranceCoverageTopSellingEndpoint<T>(elementNumber: number, insuranceCoverageCodes: string) { 
       const endpointUrl = `${this.dashboardInsuranceCoverageTopSellingUrl}/${elementNumber}` + '/?insuranceCategoryPolicyCodes='+ insuranceCoverageCodes;
       
       return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
        catchError(error => {
          return this.handleError(error, () => this.getDashboardInsuranceCoverageTopSellingEndpoint<T>(elementNumber, insuranceCoverageCodes));
        }));
    }

    getDashboardInsuranceCoverageOtherEndpoint<T>(insuranceCoverageCodes: string) { 
      const endpointUrl = this.dashboardInsuranceCoverageOtherUrl + '/?insuranceCategoryPolicyCodes='+ insuranceCoverageCodes;
      
      return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
       catchError(error => {
         return this.handleError(error, () => this.getDashboardInsuranceCoverageOtherEndpoint<T>(insuranceCoverageCodes));
       }));
    }

    getDashboardInsuranceCoverageChartEndpoint<T>(customerCode: string) { 
      const endpointUrl = `${this.dashboardInsuranceCoverageChartUrl}${customerCode}`;
   
    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
      catchError(error => {
        return this.handleError(error, () => this.getDashboardInsuranceCoverageChartEndpoint<T>(customerCode));
      }));
   }
}
