import { Injectable } from '@angular/core';
import { CustomerEdit, CustomerGrid } from '../models/customer';
import { CustomerEndpointService } from './customer-endpoint.service';
import { Observable, Subject } from 'rxjs';
import { tap } from 'rxjs';

export type CustomerChangedOperation = 'add' | 'delete' | 'modify';
export interface CustomersChangedEventArg { customers: CustomerGrid[] | string[]; operation: CustomerChangedOperation; }

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
    return this.customerEndpoint.getCustomersEndpoint<CustomerGrid>();    
  }

  getCustomer(taxIdCode: string): Observable<CustomerEdit> {
    return this.customerEndpoint.getCustomerEndpoint(taxIdCode);
  }
  
  deleteCustomer(data: CustomerGrid): Observable<CustomerGrid> {
      return this.customerEndpoint.getDeleteCustomerEndpoint<CustomerGrid>(data.taxIdCode).pipe<CustomerGrid>(
        tap(data => this.onCustomersChanged([data], CustomerService.customerDeletedOperation)));    
  }

  private onCustomersChanged(customers: CustomerGrid[] | string[], op: CustomerChangedOperation) {
    this.customersChanged.next({ customers, operation: op });
  }
}
