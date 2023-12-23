import { Component } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {
  get canViewCustomers() {
    // return this.accountService.userHasPermission(Permission.viewUsers); // eg. viewCustomersPermission
    return true;
  }

  get canViewProducts() {
    // return this.accountService.userHasPermission(Permission.viewUsers); // eg. viewProductsPermission
    return true;
  }

  get canViewOrders() {
    return true; // eg. viewOrdersPermission
  }
}
