import { Injectable } from '@angular/core';
import { Customer } from '../models/customer';
import { CustomerEndpointService } from './customer-endpoint.service';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  constructor(private customerEndpoint: CustomerEndpointService) { }

  getCustomers() {
    return this.customerEndpoint.getCustomersEndpoint<Customer>();    
  }
}
