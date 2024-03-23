import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { error } from 'console';
import { BootstrapTabDirective, EventArg } from 'src/app/directives/bootstrap-tab.directive';
import { AddressDetail, CustomerDetail } from 'src/app/models/customer';
import { AlertService, MessageSeverity } from 'src/app/services/alert.service';
import { fadeInOut } from 'src/app/services/animations';
import { DashboardServiceService } from 'src/app/services/dashboard-service.service';
import { Utilities } from 'src/app/services/utilities';
import { DeliveryDetailComponent } from '../delivery-detail/delivery-detail.component';
import { AddressDetailComponent } from '../address-detail/address-detail.component';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.scss'],
  animations: [fadeInOut]
})
export class CustomerDetailComponent implements OnInit, OnDestroy {
  isRegistryActivated = true;
  isAddressActivated = false;
  isDeliveryActivated = false;
  isJobActivated = false;
  
  readonly registryTab = 'registry';
  readonly addressTab = 'address';
  readonly deliveryTab = 'delivery';
  readonly jobTab = 'job';

  @Input()
  customerCode = "";
  
  @Input()
  showDetail: boolean;

  customerDetail: CustomerDetail;

  @ViewChild('tab', { static: true })
  tab!: BootstrapTabDirective;
  
  fragmentSubscription: Subscription | undefined;

  constructor(private dashboardService: DashboardServiceService, private router: Router, private route: ActivatedRoute, private alertService: AlertService) { }

  ngOnInit(): void {
     this.loadData();
     this.fragmentSubscription = this.route.fragment.subscribe(anchor => this.showContent(anchor));
  }

  ngOnDestroy(): void {
    this.fragmentSubscription?.unsubscribe(); 
  }

  loadData(): void{
    this.dashboardService.getCustomerDetail(this.customerCode)
        .subscribe({
          next: result => this.onDataLoadSuccessfull(result),
          error: error => this.onDataLoadFailed(error)
        });
  }

  onDataLoadSuccessfull(data: CustomerDetail) {
    this.alertService.stopLoadingMessage();
    this.customerDetail = data;
    this.showContent('registry');
  }

  onDataLoadFailed(error: HttpErrorResponse) {
    this.alertService.stopLoadingMessage();

    this.alertService.showStickyMessage('Load Error',
      `Unable to retrieve users from the server.\r\nError: "${Utilities.getHttpResponseMessage(error)}"`,
      MessageSeverity.error, error);
  }

  showContent(anchor: string | null) {
    if (anchor) {
      anchor = anchor.toLowerCase();
    }
    
    this.tab.show(`#${anchor || this.registryTab}Tab`);
  }

  isFragmentEquals(fragment1: string | null, fragment2: string | null) {
    if (fragment1 == null) {
      fragment1 = '';
    }

    if (fragment2 == null) {
      fragment2 = '';
    }

    return fragment1.toLowerCase() === fragment2.toLowerCase();
  }

  onShowTab(event: EventArg) {
    const activeTab = (event.target as HTMLAnchorElement).hash.split('#', 2).pop();

    this.isRegistryActivated = activeTab === this.registryTab;
    this.isAddressActivated = activeTab === this.addressTab;
    this.isDeliveryActivated = activeTab === this.deliveryTab;
    this.isJobActivated = activeTab === this.jobTab;

    this.router.navigate([], { fragment: activeTab });
  }
}
