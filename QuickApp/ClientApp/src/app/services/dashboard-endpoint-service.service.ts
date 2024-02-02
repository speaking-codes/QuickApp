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
    get dashboardTitleUrl() { return this.configurations.baseUrl + '/api/Dashboard/Title/'; }

    constructor(private configurations: ConfigurationService, http: HttpClient, authService: AuthService) {  super(http, authService); }

    getCustomerHeaderEndpoint<T>(customerCode: string): Observable<T> {
      const endpointUrl = `${this.dashboardCustomerHeaderUrl}/${customerCode}`;
  
      return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
        catchError(error => {
          return this.handleError(error, () => this.getCustomerHeaderEndpoint<T>(customerCode));
        }));
    }

    getCustomerDetailEndpoint<T>(customerCode: string): Observable<T> {
      const endpointUrl = `${this.dashboardCustomerDetailUrl}/${customerCode}`;
  
      return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
        catchError(error => {
          return this.handleError(error, () => this.getCustomerDetailEndpoint<T>(customerCode));
        }));
    }

    getDashboardTitleEndpoint<T>(customerCode: string): Observable<T> {
      const endpointUrl = `${this.dashboardTitleUrl}/${customerCode}`;
  
      return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
        catchError(error => {
          return this.handleError(error, () => this.getDashboardTitleEndpoint<T>(customerCode));
        }));
    }
}
