import { Injectable } from '@angular/core';
import { Customer } from '../models/customer';
import { CustomerEndpointService } from './customer-endpoint.service';
import { Observable, Subject } from 'rxjs';
import { tap } from 'rxjs';

export type CustomerChangedOperation = 'add' | 'delete' | 'modify';
export interface CustomersChangedEventArg { customers: Customer[] | string[]; operation: CustomerChangedOperation; }

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  public static readonly customerAddedOperation: CustomerChangedOperation = 'add';
  public static readonly customerDeletedOperation: CustomerChangedOperation = 'delete';
  public static readonly customerModifiedOperation: CustomerChangedOperation = 'modify';
  
  private customersChanged = new Subject<CustomersChangedEventArg>();
  
  constructor(private customerEndpoint: CustomerEndpointService) { }

  getCustomers() {
    return this.customerEndpoint.getCustomersEndpoint<Customer>();    
  }

  deleteCustomer(data: Customer): Observable<Customer> {
      return this.customerEndpoint.getDeleteCustomerEndpoint<Customer>(data.taxIdCode).pipe<Customer>(
        tap(data => this.onCustomersChanged([data], CustomerService.customerDeletedOperation)));    
  }

  private onCustomersChanged(customers: Customer[] | string[], op: CustomerChangedOperation) {
    this.customersChanged.next({ customers, operation: op });
  }
}
