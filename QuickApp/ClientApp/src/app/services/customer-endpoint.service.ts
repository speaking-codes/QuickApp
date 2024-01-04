import { Injectable } from '@angular/core';
import { EndpointBase } from './endpoint-base.service';
import { ConfigurationService } from './configuration.service';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs';
import { error } from 'console';
import { Observable } from 'rxjs';
import { CustomerEdit } from '../models/customer';

@Injectable({
  providedIn: 'root'
})
export class CustomerEndpointService extends EndpointBase {
  get customersUrl() { return this.configurations.baseUrl + '/api/Customer';}
  get customerForEditUrl() { return this.configurations.baseUrl + '/api/Customer/foredit'; }
  get customersActiveUrl() { return this.configurations.baseUrl + '/api/Customer/Active';}
  get customerByTaxIdCode() { return this.configurations.baseUrl + '/api/Customer/taxidcode'; }
 
  constructor(private configurations: ConfigurationService, http: HttpClient, authService: AuthService) {
    super(http, authService);
  }

  getCustomersEndpoint<T>() {
    return this.http.get<T>(this.customersUrl, this.requestHeaders).pipe(
      catchError(error =>{
        console.log('error');
        return this.handleError(error, () => this.getCustomersEndpoint<T>())
      })
    );
  }

  getCustomerEndpoint<T>(taxIdCode: string): Observable<T> {
    const endpointUrl = `${this.customerForEditUrl}/${taxIdCode}`;

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

  getNewCustomerEndpoint<T>(customer: CustomerEdit): Observable<T>{    
    const endpointUrl = this.customersUrl;
    return this.http.post<T>(endpointUrl, JSON.stringify(customer), this.requestHeaders).pipe(
      catchError(error => {
        return this.handleError(error, () => this.getNewCustomerEndpoint<T>(customer));
      })
    );
  }  

  getUpdateCustomerEndpoint<T>(taxIdCode:string, customer: CustomerEdit): Observable<T>{
    const endpointUrl = '${this.customerForEditUrl}/${taxIdCode}';
    return this.http.put<T>(endpointUrl, JSON.stringify(customer), this.requestHeaders).pipe(
      catchError(error => {
        return this.handleError(error, () => this.getUpdateCustomerEndpoint<T>(taxIdCode, customer));
      })
    );
  }  
}
