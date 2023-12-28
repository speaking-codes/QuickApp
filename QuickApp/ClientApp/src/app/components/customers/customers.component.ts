// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { TableColumn } from '@swimlane/ngx-datatable';
import { SearchBoxComponent } from '../controls/search-box.component';

import { Customer } from 'src/app/models/customer';
import { Role } from 'src/app/models/role.model';

import { fadeInOut } from '../../services/animations';
import { CustomerService } from 'src/app/services/customer.service';
import { AlertService, DialogType, MessageSeverity } from 'src/app/services/alert.service';
import { Utilities } from 'src/app/services/utilities';

interface CustomerIndex extends Customer{
  index: number;
}

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss'],
  animations: [fadeInOut]
})
export class CustomersComponent implements OnInit{
  columns: TableColumn[] = [];
  rows: Customer[] = [];
  rowsCache: Customer[] = [];
  loadingIndicator = false;  
  
  constructor(private customerService: CustomerService, private alertService: AlertService){}
  
  ngOnInit(): void {
    this.columns = [
      { prop: 'index', name: '#', width: 40, canAutoResize: false, sortable: false },
      { prop: 'fullName', name: 'Nominativo', canAutoResize: false, sortable: true },
      { prop: 'birthDate', name: 'Data di Nascita'},
      { prop: 'taxIdCode', name: 'Codice Fiscale', canAutoResize: false, sortable: true },
      { prop: 'gender', name: 'Sesso', canAutoResize: false, sortable: false},
      { prop: 'city', name: 'Residenza', canAutoResize: false, sortable: false},
      { prop: 'details', name: '', canAutoResize: false, sortable: false}
    ];

    this.loadData();
  }

  loadData(): void{
    this.loadingIndicator = true;
 
    this.customerService.getCustomers()
        .subscribe({
          next: results => this.onDataLoadSuccessful(results),
          error: error => this.onDataLoadFailed(error)
        });
  }

  onDataLoadSuccessful(customers: Customer[]) {
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;

    customers.forEach((customer, index) => {
      (customer as CustomerIndex).index = index + 1;
    });

    this.rowsCache = [...customers];
    this.rows = customers;
  }
  
  onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }
}
