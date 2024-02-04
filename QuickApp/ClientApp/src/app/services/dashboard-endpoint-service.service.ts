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
}
