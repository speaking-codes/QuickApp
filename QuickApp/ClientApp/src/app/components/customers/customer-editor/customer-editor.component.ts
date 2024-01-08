import { Component, ViewChild, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Customer, CustomerEdit, CustomerGrid, CustomerDetailHeader } from 'src/app/models/customer';
import { EnumGender } from 'src/app/models/enums';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { CustomerService } from 'src/app/services/customer.service';
import { HttpErrorResponse } from '@angular/common/http';
import { error } from 'console';
import { Utilities } from 'src/app/services/utilities';
import { TableColumn } from '@swimlane/ngx-datatable';

@Component({
  selector: 'app-customer-editor',
  templateUrl: './customer-editor.component.html',
  styleUrls: ['./customer-editor.component.scss']
})

export class CustomerEditorComponent implements OnInit, OnDestroy {
  private addressColumns: TableColumn[] = [];
  // private addressRows: Address[] = [];
  // private addressRowsCache: Address[] = [];  
  // private deliveryColumns: TableColumn[] = [];
  // private deliveryRows: Delivery[] = [];
  // private deliveryRowsCache: Delivery[] = [];  
  private isNewCustomer: boolean = true;
  private customer: Customer | null;
  private customerEdit: CustomerEdit = new CustomerEdit();
  private showValidationErrors: boolean = true;
  private  loadingIndicator = false;  

  public isSaving: boolean = false;
  public formResetToggle = true;
  
  public changesSavedCallback: { (): void } | undefined;
  public changesFailedCallback: { (): void } | undefined;
  public changesCancelledCallback: { (): void } | undefined;

  public customerGrid = new CustomerGrid();

  public isEditMode: boolean = false;
  
  @Input()
  isViewOnly = false;

  @Output()
  afterOnInit = new EventEmitter<CustomerEditorComponent>();

  @ViewChild('f')
  private form!: NgForm;
  
  get IsFieldReadOnly() { return !this.isEditMode; }

  constructor(private customerService: CustomerService, private alertService: AlertService) { }

  ngOnInit(): void {
    this.afterOnInit.emit(this);
  }

  ngOnDestroy(): void {    
  }

  newCustomer(){
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;
    this.isNewCustomer = true;
    this.edit();

    return this.customerEdit;
  }

  detailCustomer(customer: Customer){
    this.customerService.getCustomer(customer.customerCode)
        .subscribe({
            next: customer => this.onDataLoadSuccessFull(customer),
            error: error => this.onDataLoadFailed(error)
        });
    return this.customerEdit;
  }

  editCustomer(){
    this.isEditMode = true;
  }
  private edit(){
    if (!this.isNewCustomer){
      this.customerEdit = new CustomerEdit();      
      Object.assign(this.customerEdit, this.customer);
      this.isEditMode = false; 
     } else {
      if (!this.customerEdit) {
        this.customerEdit = new CustomerEdit();               
      }      
      this.isEditMode = true; 
    }  
  }

  private onDataLoadSuccessFull(customer: CustomerEdit)
  {
    this.alertService.stopLoadingMessage();
    this.loadingIndicator = false;
    
    if (customer) {
      this.isNewCustomer = false;
      this.customer = new Customer();
      this.customerEdit = new CustomerEdit();
      
      Object.assign(this.customer, customer);
      Object.assign(this.customerEdit, customer);
        this.edit();

      return this.customerEdit;
    } else {
      return this.newCustomer();
    }
  }

  private onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve customer data from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);

    this.customerEdit = new CustomerEdit();
  }

  cancel(){
    this.customerEdit = new CustomerEdit();

    this.showValidationErrors = false;
    this.resetForm();

    this.alertService.showMessage('Cancelled', 'Operation cancelled by user', MessageSeverity.default);
    this.alertService.resetStickyMessage();

    if (this.changesCancelledCallback) {
      this.changesCancelledCallback();
    }
  }

  resetForm(replace = false) {
    if (!replace) {
      this.form.reset();
    } else {
      this.formResetToggle = false;

      setTimeout(() => {
        this.formResetToggle = true;
      });
    }
  }

  
  save() {
    this.isSaving = true;
    this.alertService.startLoadingMessage('Saving changes...');   

    if (this.isNewCustomer) {
      console.log('Insert new customer');
      this.customerService.newCustomer(this.customerEdit)
        .subscribe({ next: role => this.saveSuccessHelper(this.customerEdit), error: error => this.saveFailedHelper(error) });
    } else {
      console.log('Update customer');
      this.customerService.updateCustomer(this.customerEdit)
        .subscribe({ next: () => this.saveSuccessHelper(this.customerEdit), error: error => this.saveFailedHelper(error) });
    }
  }

  private saveSuccessHelper(customer: CustomerEdit)
  {
    if (customer) {
      Object.assign(this.customerEdit, customer);
    }

    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.showValidationErrors = false;

    if (this.isNewCustomer) {
      this.alertService.showMessage('Success', `Customer "${this.customerEdit.firstName}" "${this.customerEdit.lastName}" was created successfully`, MessageSeverity.success);
    } else {
      this.alertService.showMessage('Success', `Changes to Customer "${this.customerEdit.firstName}" "${this.customerEdit.lastName}" was saved successfully`, MessageSeverity.success);
    }

    this.customerEdit = new CustomerEdit();
    this.resetForm();

    if (this.changesSavedCallback) {
      this.changesSavedCallback();
    }
  }

  private saveFailedHelper(error: HttpErrorResponse) {
    this.isSaving = false;
    this.alertService.stopLoadingMessage();
    this.alertService.showStickyMessage('Save Error', 'The below errors occurred whilst saving your changes:', MessageSeverity.error, error);
    this.alertService.showStickyMessage(error, null, MessageSeverity.error);

    if (this.changesFailedCallback) {
      this.changesFailedCallback();
    }
  }

  // deleteAddress(data: Address){
  //   this.addressRows.forEach((value,index)=>{
  //     if(value.$$index == data.$$index) 
  //         this.addressRows.splice(index,1);
  //   });
  // }

  // deleteDelivery(data: Delivery){
  //   this.deliveryRows.forEach((value,index)=>{
  //     if(value.$$index == data.$$index) 
  //         this.deliveryRows.splice(index,1);
  //   });
  // }
}
