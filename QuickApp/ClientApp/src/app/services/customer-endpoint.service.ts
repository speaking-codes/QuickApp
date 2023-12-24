import { Injectable } from '@angular/core';
import { EndpointBase } from './endpoint-base.service';
import { ConfigurationService } from './configuration.service';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs';
import { error } from 'console';

@Injectable({
  providedIn: 'root'
})
export class CustomerEndpointService extends EndpointBase {
  get customersUrl() { return this.configurations.baseUrl + '/api/Customer';}
  get customerByTaxIdCode() { return this.configurations.baseUrl + '/api/Customer/taxidcode'; }

  constructor(private configurations: ConfigurationService, http: HttpClient, authService: AuthService) {
    super(http, authService);
  }

  getCustomersEndpoint<T>(){
    console.log('endpoint url:' + this.customersUrl);
    return this.http.get<T>(this.customersUrl, this.requestHeaders).pipe(
      catchError(error =>{
        console.log('error');
        return this.handleError(error, () => this.getCustomersEndpoint<T>())
      })
    );
  }
}
