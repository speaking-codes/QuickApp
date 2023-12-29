// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------
// ---------------------------------------

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/services/auth.service';
import { AppTranslationService } from 'src/app/services/app-translation.service';
import { LocalStoreManager } from 'src/app/services/local-store-manager.service';

import { Component, OnInit, OnDestroy, Input, TemplateRef, ViewChild } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { TableColumn } from '@swimlane/ngx-datatable';
import { SearchBoxComponent } from '../controls/search-box.component';
import { ColumnMode, SelectionType } from '@swimlane/ngx-datatable';
import { DatatableComponent } from '@swimlane/ngx-datatable';

import { Customer } from 'src/app/models/customer';

import { fadeInOut } from '../../services/animations';
import { CustomerService } from 'src/app/services/customer.service';
import { AlertService, DialogType, MessageSeverity } from 'src/app/services/alert.service';
import { Utilities } from 'src/app/services/utilities';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss'],
  animations: [fadeInOut]
})
export class CustomersComponent implements OnInit, OnDestroy {
  columns: TableColumn[] = [];
  rows: Customer[] = [];
  rowsCache: Customer[] = [];
  loadingIndicator = false;  
  
  @Input()
  verticalScrollbar = false;
  
  @Input()
  pageSize = 12;

  @ViewChild(DatatableComponent) table: DatatableComponent;

  constructor(private customerService: CustomerService, private alertService: AlertService){}
  
  isCustomerActive(data: Customer) { return data.isActive; }

  isCustomerMale(data: Customer) { return data.gender.toLowerCase() === "uomo"; }

  ngOnInit(): void {
    this.columns = [
      { prop: 'index', name: '#', width: 40, canAutoResize: false, sortable: false },
      { prop: 'fullName', name: 'Nominativo', canAutoResize: false, sortable: true },
      { prop: 'birthDate', name: 'Data di Nascita'},
      { prop: 'taxIdCode', name: 'Codice Fiscale', canAutoResize: false, sortable: true },
      { prop: 'gender', name: 'Sesso', canAutoResize: false, sortable: false},
      { prop: 'city', name: 'Residenza', canAutoResize: false, sortable: false},
      { prop: 'state', name: 'Stato', canAutoResize: false, sortable: false },
      { prop: 'actions', name: 'Azioni', canAutoResize: false, sortable: false}
    ];

    this.loadData();
  }

  ngOnDestroy(): void {
    
  }

  loadData(): void{
    this.loadingIndicator = true;
 
    this.customerService.getCustomers()
        .subscribe({
          next: results => this.onDataLoadSuccessful(results),
          error: error => this.onDataLoadFailed(error)
        });
  }

  onDataLoadSuccessful(data: Customer[]) {
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;
    this.refreshDataIndexes(data);
    this.rows = data;
    this.rowsCache = [...data];
  }
  
  onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }

  updateFilter(searchValue) {
    if (!searchValue)
    {
      this.rows = this.rowsCache;
      this.table.offset = 0;
      return
    }
    
    this.rows = this.rowsCache.filter(function (c) { Utilities.searchArray(searchValue, false, c.fullName); });
    this.table.offset = 0;
  }

  refreshDataIndexes(data: Customer[]) {
    let index = 0;

    for (const i of data) {
      i.$$index = index++;
    }
  }

  detailsCustomer(data: Customer){
    console.log('Details of Customer: ' + data.fullName);
  }

  editCustomer(data: Customer){
    console.log('Edit Customer: ' + data.fullName);
  }

  deleteCustomer(data: Customer){
    console.log('Delete Customer: ' + data.fullName);
  }
}
