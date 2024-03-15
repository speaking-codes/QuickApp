// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DAL.Core
{
    public static class ApplicationPermissions
    {
        public static ReadOnlyCollection<ApplicationPermission> AllPermissions;

        public const string UsersPermissionGroupName = "User Permissions";
        public static ApplicationPermission ViewUsers = new ApplicationPermission("View Users", "users.view", UsersPermissionGroupName, "Permission to view other users account details");
        public static ApplicationPermission ManageUsers = new ApplicationPermission("Manage Users", "users.manage", UsersPermissionGroupName, "Permission to create, delete and modify other users account details");

        public const string RolesPermissionGroupName = "Role Permissions";
        public static ApplicationPermission ViewRoles = new ApplicationPermission("View Roles", "roles.view", RolesPermissionGroupName, "Permission to view available roles");
        public static ApplicationPermission ManageRoles = new ApplicationPermission("Manage Roles", "roles.manage", RolesPermissionGroupName, "Permission to create, delete and modify roles");
        public static ApplicationPermission AssignRoles = new ApplicationPermission("Assign Roles", "roles.assign", RolesPermissionGroupName, "Permission to assign roles to users");

        public const string CustomerPermissionGroupName = "Customer Permissions";
        public static ApplicationPermission ViewCustomers = new ApplicationPermission("View Customers", "customers.view", CustomerPermissionGroupName, "Permission to view customers registered");
        public static ApplicationPermission ManageCustomers = new ApplicationPermission("Manage Customers", "customers.manage", CustomerPermissionGroupName, "Permission to create, delete and modify customers");
        public static ApplicationPermission ViewDashboardCustomers = new ApplicationPermission("View Dashboard Customers", "customers.viewdashboard", CustomerPermissionGroupName, "Permission to view the customers' dashboard");

        static ApplicationPermissions()
        {
            var allPermissions = new List<ApplicationPermission>
            {
                ViewUsers,
                ManageUsers,

                ViewRoles,
                ManageRoles,
                AssignRoles,

                ViewCustomers,
                ManageCustomers,
                ViewDashboardCustomers
            };

            AllPermissions = allPermissions.AsReadOnly();
        }

        public static ApplicationPermission GetPermissionByName(string permissionName)
        {
            return AllPermissions.SingleOrDefault(p => p.Name == permissionName);
        }

        public static ApplicationPermission GetPermissionByValue(string permissionValue)
        {
            return AllPermissions.SingleOrDefault(p => p.Value == permissionValue);
        }

        public static IList<string> GetAllPermissionValues()
        {
            return AllPermissions.Select(p => p.Value).ToArray();
        }

        public static IList<string> GetEditorPermissionValues()
        {
            return new List<string> { ViewCustomers, ManageCustomers };
        }

        public static IList<string> GetAgentPermissionValues()
        {
            return new List<string> { ViewCustomers, ViewDashboardCustomers };
        }

        public static IList<string> GetAdministrativePermissionValues()
        {
            return new string[] { ManageUsers, ManageRoles, AssignRoles };
        }
    }

    public class ApplicationPermission
    {
        public ApplicationPermission()
        { }

        public ApplicationPermission(string name, string value, string groupName, string description = null)
        {
            Name = name;
            Value = value;
            GroupName = groupName;
            Description = description;
        }

        public string Name { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(ApplicationPermission permission)
        {
            return permission.Value;
        }
    }
}
