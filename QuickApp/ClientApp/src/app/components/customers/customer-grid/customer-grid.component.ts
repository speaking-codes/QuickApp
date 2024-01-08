import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/services/auth.service';
import { AppTranslationService } from 'src/app/services/app-translation.service';
import { LocalStoreManager } from 'src/app/services/local-store-manager.service';

import { Component, OnInit, OnDestroy, Input, TemplateRef, ViewChild } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { TableColumn } from '@swimlane/ngx-datatable';
import { SearchBoxComponent } from '../../controls/search-box.component';
import { ColumnMode, SelectionType } from '@swimlane/ngx-datatable';
import { DatatableComponent } from '@swimlane/ngx-datatable';

import { Customer, CustomerGrid, CustomerEdit } from 'src/app/models/customer';
import { CustomerEditorComponent } from '../customer-editor/customer-editor.component';

import { fadeInOut } from 'src/app/services/animations';
import { CustomerService } from 'src/app/services/customer.service';
import { AlertService, DialogType, MessageSeverity } from 'src/app/services/alert.service';
import { Utilities } from 'src/app/services/utilities';
import { EnumLoadGridStep } from 'src/app/models/enums';

@Component({
  selector: 'app-customer-grid',
  templateUrl: './customer-grid.component.html',
  styleUrls: ['./customer-grid.component.scss'],
  animations: [fadeInOut]
})
export class CustomerGridComponent implements OnInit, OnDestroy {
  columns: TableColumn[] = [];
  rows: CustomerGrid[] = [];
  rowsCache: CustomerGrid[] = [];  
  editedCustomer: CustomerEdit | null = null;
  sourceCustomer: CustomerGrid | null = null;
  editingCustomerName: { name: string } | null = null;
  loadingIndicator = false;  
  loadGridStep: EnumLoadGridStep;

  @Input()
  verticalScrollbar = false;
  
  @Input()
  pageSize = 12;

  @ViewChild(DatatableComponent) table: DatatableComponent;

  @ViewChild('editorModal', { static: true })
  editorModalTemplate!: TemplateRef<unknown>;
  
  customerEditor: CustomerEditorComponent | null = null;

  constructor(private customerService: CustomerService, private alertService: AlertService, private modalService: NgbModal){}
  
  get isNewCustomer() { return this.sourceCustomer == null; }

  get hideDeletedCustomer() { return (this.loadGridStep == EnumLoadGridStep.First || this.loadGridStep == EnumLoadGridStep.ShowOnlyActive); }

  updateLoadGridStep(){
    switch(this.loadGridStep){
      case EnumLoadGridStep.First:
        this.loadGridStep = EnumLoadGridStep.ShowAll;
        break;
      case EnumLoadGridStep.ShowAll:
        this.loadGridStep = EnumLoadGridStep.ShowOnlyActive;
          break;
      case EnumLoadGridStep.ShowOnlyActive:
        this.loadGridStep = EnumLoadGridStep.ShowAll;
        break;
      default:
        this.loadGridStep = EnumLoadGridStep.First;
        break;
    }
  }

  isCustomerActive(data: CustomerGrid) { return data.isActive; }

  isCustomerMale(data: CustomerGrid) { return data.gender.toLowerCase() === "uomo"; }

  ngOnInit(): void {
    this.columns = [
      { prop: 'index', name: '#', width: 40, canAutoResize: false, sortable: false },
      { prop: 'fullName', name: 'Nominativo', canAutoResize: false, sortable: true },
      { prop: 'birthDate', name: 'Data di Nascita'},
      { prop: 'customerCode', name: 'Codice Fiscale', canAutoResize: false, sortable: true },
      { prop: 'gender', name: 'Sesso', canAutoResize: false, sortable: false},
      { prop: 'city', name: 'Residenza', canAutoResize: false, sortable: false},
      { prop: 'state', name: 'Stato', canAutoResize: false, sortable: false },
      { prop: 'actions', name: 'Azioni', canAutoResize: false, sortable: false}
    ];

    this.loadData();
  }

  ngOnDestroy(): void {
    
  }

  setCustomerEditorComponent(customerEditor: CustomerEditorComponent){
    this.customerEditor = customerEditor;

    if (this.sourceCustomer == null)
      this.editedCustomer = this.customerEditor.newCustomer();
    else
      this.editedCustomer = this.customerEditor.detailCustomer(this.sourceCustomer);
  }

  loadData(): void{
    this.loadingIndicator = true;    

    this.customerService.getCustomers()
        .subscribe({
          next: results => this.onDataLoadSuccessful(results),
          error: error => this.onDataLoadFailed(error)
        });        
  }

  onDataLoadSuccessful(data: CustomerGrid[]) {
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;
    this.updateLoadGridStep();
    this.refreshDataIndexes(data);
    this.rowsCache = [...data];    
    this.rows = this.getCustomersFilterActive();    

    setTimeout(() => { this.loadingIndicator = false; }, 1500);    
  }
  
  onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }

  updateFilter(searchValue: string) {
    if (!searchValue)
    {
      this.rows = this.getCustomersFilterActive();
      this.table.offset = 0;
      return
    }
    
    var tempRows = this.getCustomersFilterActive();
    this.rows = tempRows.filter(function (c) { return Utilities.searchArray(searchValue, false, c.fullName); });
    this.table.offset = 0;
  }

  refreshDataIndexes(data: CustomerGrid[]) {
    let index = 0;

    for (const i of data) {
      i.$$index = index++;
    }
  }

  detailsCustomer(data: CustomerGrid){
    this.editingCustomerName = { name: data.fullName };
    this.sourceCustomer = data;

    this.openCustomerEditor();
  }

  editCustomer(data: CustomerGrid){    
    this.editingCustomerName = { name: data.fullName };
    this.sourceCustomer = data;

    this.openCustomerEditor();
  }

  openCustomerEditor() {
    const modalRef = this.modalService.open(this.editorModalTemplate, {
      size: 'lg',
      backdrop: 'static'
    });

    modalRef.shown.subscribe(() => {
      if (!this.customerEditor)
        throw new Error('The customer editor component was not set.');

      this.customerEditor.changesSavedCallback = () => {
        this.addNewCustomerToList();
        modalRef.close();
      };

      this.customerEditor.changesCancelledCallback = () => {
        this.editedCustomer = null;
        this.sourceCustomer = null;
        modalRef.close();
      };
    });
  }

  addNewCustomerToList() {
    if (this.sourceCustomer) {
        Object.assign(this.sourceCustomer, this.editedCustomer);

        let sourceIndex = this.rowsCache.indexOf(this.sourceCustomer, 0);
        if (sourceIndex > -1) {
          Utilities.moveArrayItem(this.rowsCache, sourceIndex, 0);
        }

        sourceIndex = this.rows.indexOf(this.sourceCustomer, 0);
        if (sourceIndex > -1) {
          Utilities.moveArrayItem(this.rows, sourceIndex, 0);
        }

        this.editedCustomer = null;
        this.sourceCustomer = null;
    } else {
        const customer = new CustomerGrid();
        //Object.assign(customer, this.editedCustomer);
        customer.birthDate = this.editedCustomer.birthDate;
        customer.customerCode = this.editedCustomer.customerCode;
        customer.fullName = this.editedCustomer.firstName + this.editedCustomer.lastName;
        customer.gender = this.editedCustomer.gender.toString();
        customer.residence = this.editedCustomer.city + '(' + this.editedCustomer.province + ')';
        customer.isActive = true;
        
        this.editedCustomer = null;

        let maxIndex = 0;
        for (const r of this.rowsCache) {
          if ((r as CustomerGrid).$$index > maxIndex) {
            maxIndex = r.$$index;
          }
        }

        customer.$$index = maxIndex + 1;

        this.rowsCache.splice(0, 0, customer);
        this.rows.splice(0, 0, customer);
        this.rows = [...this.rows];
    }
  }

  deleteCustomer(data: CustomerGrid){
    this.alertService.showDialog(`Sei sicuro di voler rimuovere il cliente: "${data.fullName}" - "${data.customerCode}" ed tutti i contratti ad esso associati?`,
      DialogType.confirm, () => this.deleteCustomerHelper(data));    
  }

  deleteCustomerHelper(data: CustomerGrid) {
    this.alertService.startLoadingMessage('Deleting...');
    this.loadingIndicator = true;

    this.customerService.deleteCustomer(data)
      .subscribe({
        next: () => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;
    
          this.rowsCache.forEach((value,index)=>{            
            if(value.$$index == data.$$index) 
                this.rowsCache[index].isActive = false;
          });

          this.rows.forEach((value,index)=>{
            if(value.$$index == data.$$index) 
                this.rows[index].isActive = false;
          });

          this.rows = this.getCustomersFilterActive();
        },
        error: error => {
          this.alertService.stopLoadingMessage();
          this.loadingIndicator = false;

          this.alertService.showStickyMessage('Delete Error',
            `An error occurred whilst deleting the customer.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);
        }
      });
  }

  newCustomer() { 
    this.editingCustomerName = null;
    this.sourceCustomer = null;
    this.openCustomerEditor();
  }

  getCustomersFilterActive(){
    if (this.hideDeletedCustomer)
      return this.rowsCache.filter(function(item){ return item.isActive; });      
    else
      return this.rowsCache;
  }

  showActiveCustomers() { 
     this.updateLoadGridStep();
     this.rows = this.getCustomersFilterActive();     
  } 
}
