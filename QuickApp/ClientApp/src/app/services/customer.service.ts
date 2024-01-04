import { Injectable } from '@angular/core';
import { Customer, CustomerEdit, CustomerGrid } from '../models/customer';
import { CustomerEndpointService } from './customer-endpoint.service';
import { Observable, Subject } from 'rxjs';
import { tap } from 'rxjs';

export type CustomerChangedOperation = 'add' | 'delete' | 'modify';
export interface CustomersChangedEventArg { customers: CustomerGrid[] | string[]; operation: CustomerChangedOperation; }
export interface CustomerChangedEventArg { customer: CustomerEdit | string; operation: CustomerChangedOperation; }

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  public static readonly customerAddedOperation: CustomerChangedOperation = 'add';
  public static readonly customerDeletedOperation: CustomerChangedOperation = 'delete';
  public static readonly customerModifiedOperation: CustomerChangedOperation = 'modify';
  
  private customersChanged = new Subject<CustomersChangedEventArg>();
  private customerChanged = new Subject<CustomerChangedEventArg>();

  constructor(private customerEndpoint: CustomerEndpointService) { }

  getCustomers() {
    return this.customerEndpoint.getCustomersEndpoint<CustomerGrid>();    
  }

  getCustomer(taxIdCode: string): Observable<CustomerEdit> {
    return this.customerEndpoint.getCustomerEndpoint(taxIdCode);
  }
  
  deleteCustomer(data: CustomerGrid): Observable<CustomerGrid> {
      return this.customerEndpoint.getDeleteCustomerEndpoint<CustomerGrid>(data.taxIdCode).pipe<CustomerGrid>(
        tap(data => this.onCustomersChanged([data], CustomerService.customerDeletedOperation)));    
  }

  updateCustomer(customer: CustomerEdit) {
      return this.customerEndpoint.getUpdateCustomerEndpoint(customer.taxIdCode, customer).pipe(
        tap(() => this.onCustomerChanged(customer, CustomerService.customerModifiedOperation)));    
  }

  newCustomer(customer: CustomerEdit) {
    return this.customerEndpoint.getNewCustomerEndpoint<CustomerEdit>(customer).pipe<CustomerEdit>(
      tap(() => this.onCustomerChanged(customer, CustomerService.customerAddedOperation)));
  }

  private onCustomersChanged(customers: CustomerGrid[] | string[], op: CustomerChangedOperation) {
    this.customersChanged.next({ customers, operation: op });
  }

  private onCustomerChanged(customer: CustomerEdit | string, op: CustomerChangedOperation) {
    this.customerChanged.next({ customer, operation: op });
  }
}
