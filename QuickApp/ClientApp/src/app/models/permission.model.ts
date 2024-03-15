// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

export type PermissionNames =
  'View Users' | 'Manage Users' |
  'View Roles' | 'Manage Roles' | 'Assign Roles' |
  'View Customers' | 'Manage Customers' | 'View Dashboard Customers';

export type PermissionValues =
  'users.view' | 'users.manage' |
  'roles.view' | 'roles.manage' | 'roles.assign' |
  'customers.view' | 'customers.manage' | 'customers.viewdashboard';

export interface Permission {
  name: PermissionNames;
  value: PermissionValues;
  groupName: string;
  description: string;
}

export class Permission {
  public static readonly viewUsers: PermissionValues = 'users.view';
  public static readonly manageUsers: PermissionValues = 'users.manage';

  public static readonly viewRoles: PermissionValues = 'roles.view';
  public static readonly manageRoles: PermissionValues = 'roles.manage';
  public static readonly assignRoles: PermissionValues = 'roles.assign';

  public static readonly viewCustomers: PermissionValues = 'customers.view';
  public static readonly manageCustomers: PermissionValues =  'customers.manage'; 
  public static readonly viewDashboardCustomers: PermissionValues = 'customers.viewdashboard';
}
