import { Component, Input, OnInit } from '@angular/core';
import { TableColumn } from '@swimlane/ngx-datatable';
import { AddressDetail } from 'src/app/models/customer';

@Component({
  selector: 'app-address-detail',
  templateUrl: './address-detail.component.html',
  styleUrls: ['./address-detail.component.scss']
})
export class AddressDetailComponent implements OnInit {
  columns: TableColumn[] = [];
  rows: AddressDetail[] = [];
  loadingIndicator = false;  

  @Input()
  addresses: AddressDetail[];

  ngOnInit(): void {
    debugger;
    this.rows = this.addresses;
  }

  isAddressPrimary(data: AddressDetail){ return data?.isPrimary; }
}
