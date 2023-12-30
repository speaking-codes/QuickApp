import { Injectable } from '@angular/core';
import { EndpointBase } from './endpoint-base.service';
import { ConfigurationService } from './configuration.service';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs';
import { error } from 'console';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerEndpointService extends EndpointBase {
  get customersUrl() { return this.configurations.baseUrl + '/api/Customer';}
  get customersActiveUrl() { return this.configurations.baseUrl + '/api/Customer/Active';}
  get customerByTaxIdCode() { return this.configurations.baseUrl + '/api/Customer/taxidcode'; }
 
  constructor(private configurations: ConfigurationService, http: HttpClient, authService: AuthService) {
    super(http, authService);
  }

  getCustomersEndpoint<T>() {
    return this.http.get<T>(this.customersActiveUrl, this.requestHeaders).pipe(
      catchError(error =>{
        console.log('error');
        return this.handleError(error, () => this.getCustomersEndpoint<T>())
      })
    );
  }

  getCustomerEndpoint<T>(taxIdCode: string): Observable<T> {
    const endpointUrl = `${this.customersUrl}/${taxIdCode}`;

    return this.http.get<T>(endpointUrl, this.requestHeaders).pipe(
      catchError(error => {
        return this.handleError(error, () => this.getCustomerEndpoint<T>(taxIdCode));
      }));
  }
  
  getDeleteCustomerEndpoint<T>(taxIdCode: string): Observable<T> {
    const endpointUrl = `${this.customersUrl}/${taxIdCode}`;

    return this.http.delete<T>(endpointUrl, this.requestHeaders).pipe(
      catchError(error => {
        return this.handleError(error, () => this.getDeleteCustomerEndpoint<T>(taxIdCode));
      }));
  }
}
