import { Component, Input, OnInit } from '@angular/core';
import { TableColumn } from '@swimlane/ngx-datatable';
import { DeliveryDetail } from 'src/app/models/customer';

@Component({
  selector: 'app-delivery-detail',
  templateUrl: './delivery-detail.component.html',
  styleUrls: ['./delivery-detail.component.scss']
})
export class DeliveryDetailComponent implements OnInit {
  columns: TableColumn[] = [];
  rows: DeliveryDetail[] = [];
  loadingIndicator = false;  

  @Input()
  deliveries: DeliveryDetail[];

  ngOnInit(): void {
    this.rows = this.deliveries;
  }

  isDeliveryPrimary(data: DeliveryDetail) { return data?.isPrimary; }
}
