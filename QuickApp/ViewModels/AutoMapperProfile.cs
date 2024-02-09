// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using AutoMapper;
using DAL.Core;
using DAL.Enums;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using QuickApp.Helpers;
using DAL.ModelsNoSql;

namespace QuickApp.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        private IList<Address> getAddressCollection(CustomerEditViewModel customerViewModel)
        {
            if (customerViewModel.Location.IsStringEmpty() &&
                customerViewModel.City.IsStringEmpty() &&
                customerViewModel.PostalCode.IsStringEmpty() &&
                customerViewModel.Province.IsStringEmpty())
                return null;

            return new List<Address>()
            {
                //new Address
                //{
                //    AddressType = EnumAddressType.Residenza,
                //    Location = customerViewModel.Location,
                //    City = customerViewModel.City,
                //    PostalCode = customerViewModel.PostalCode,
                //    Province = customerViewModel.Province
                //}
            };
        }

        private IList<Delivery> getDeliveryCollection(CustomerEditViewModel customerViewModel)
        {
            if (customerViewModel.PhoneNumber.IsStringEmpty() && customerViewModel.Email.IsStringEmpty())
                return null;

            return new List<Delivery>() {
                new Delivery
                {
                    DeliveryType= EnumDeliveryType.Privato,
                    PhoneNumber= customerViewModel.PhoneNumber,
                    Email= customerViewModel.Email
                }
            };
        }

        private string getAddressString(IEnumerable<Address> addresses)
        {
            var returnValue=string.Empty;
            if (addresses==null || !addresses.Any())
                return returnValue;

            var municipality = addresses.FirstOrDefault(x => x.AddressType == EnumAddressType.Residenza)?.Municipality;
            if (municipality == null)
                return returnValue;

            return $"{municipality.MunicipalityName} ({municipality.Province.ProvinceAbbreviation})";
        }

        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>()
                   .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<UserViewModel, ApplicationUser>()
                .ForMember(d => d.Roles, map => map.Ignore())
                .ForMember(d => d.Id, map => map.Condition(src => src.Id != null));

            CreateMap<ApplicationUser, UserEditViewModel>()
                .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<UserEditViewModel, ApplicationUser>()
                .ForMember(d => d.Roles, map => map.Ignore())
                .ForMember(d => d.Id, map => map.Condition(src => src.Id != null));

            CreateMap<ApplicationUser, UserPatchViewModel>()
                .ReverseMap();

            CreateMap<ApplicationRole, RoleViewModel>()
                .ForMember(d => d.Permissions, map => map.MapFrom(s => s.Claims))
                .ForMember(d => d.UsersCount, map => map.MapFrom(s => s.Users != null ? s.Users.Count : 0))
                .ReverseMap();
            CreateMap<RoleViewModel, ApplicationRole>()
                .ForMember(d => d.Id, map => map.Condition(src => src.Id != null));

            CreateMap<IdentityRoleClaim<string>, ClaimViewModel>()
                .ForMember(d => d.Type, map => map.MapFrom(s => s.ClaimType))
                .ForMember(d => d.Value, map => map.MapFrom(s => s.ClaimValue))
                .ReverseMap();

            CreateMap<ApplicationPermission, PermissionViewModel>()
                .ReverseMap();

            CreateMap<IdentityRoleClaim<string>, PermissionViewModel>()
                .ConvertUsing(s => (PermissionViewModel)ApplicationPermissions.GetPermissionByValue(s.ClaimValue));

            CreateMap<Customer, CustomerViewModel>()
                .ReverseMap();

            CreateMap<Customer, CustomerEditViewModel>()
                .ForMember(d => d.Gender, map => map.MapFrom(s => s.Gender.GetDefinition()))
                .ForMember(d => d.Location, map => map.MapFrom(s => (s.Addresses == null || s.Addresses.Count == 0) ? string.Empty : s.Addresses.FirstOrDefault().Location))
                //.ForMember(d => d.City, map => map.MapFrom(s => (s.Addresses == null || s.Addresses.Count == 0) ? string.Empty : s.Addresses.FirstOrDefault().City))
                //.ForMember(d => d.PostalCode, map => map.MapFrom(s => (s.Addresses == null || s.Addresses.Count == 0) ? string.Empty : s.Addresses.FirstOrDefault().PostalCode))
                //.ForMember(d => d.Province, map => map.MapFrom(s => (s.Addresses == null || s.Addresses.Count == 0) ? string.Empty : s.Addresses.FirstOrDefault().Province))
                .ForMember(d => d.PhoneNumber, map => map.MapFrom(s => (s.Deliveries == null || s.Deliveries.Count == 0) ? string.Empty : s.Deliveries.FirstOrDefault().PhoneNumber))
                .ForMember(d => d.Email, map => map.MapFrom(s => (s.Deliveries == null || s.Deliveries.Count == 0) ? string.Empty : s.Deliveries.FirstOrDefault().Email))
                .ReverseMap()
                .ForMember(d => d.Addresses, map => map.MapFrom(s => getAddressCollection(s)))
                .ForMember(d => d.Deliveries, map => map.MapFrom(s => getDeliveryCollection(s)));

            CreateMap<Customer, CustomerGridViewModel>()
                .ForMember(d => d.FullName, map => map.MapFrom(s => $"{s.LastName} {s.FirstName}"))
                .ForMember(d => d.BirthDate, map => map.MapFrom(s => s.BirthDate.HasValue ? s.BirthDate.Value.ToString("dd MMM yyyy") : string.Empty))
                .ForMember(d => d.Gender, map => map.MapFrom(s => s.Gender.GetDefinition()))
                .ForMember(d => d.Residence, map => map.MapFrom(s => getAddressString(s.Addresses)));

            CreateMap<Customer, CustomerDetailHeaderViewModel>()
                .ForMember(d => d.FullName, map => map.MapFrom(s => $"{s.LastName} {s.FirstName}"))
                //.ForMember(d => d.Residence, map => map.MapFrom(s => (s.Addresses == null || s.Addresses.Count == 0) ? string.Empty : $"{s.Addresses[0].Location} - {s.Addresses[0].City} ({s.Addresses[0].Province})"))
                .ForMember(d => d.PhoneNumber, map => map.MapFrom(s => (s.Deliveries == null || s.Deliveries.Count == 0) ? string.Empty : s.Deliveries[0].PhoneNumber))
                .ForMember(d => d.Email, map => map.MapFrom(s => (s.Deliveries == null || s.Deliveries.Count == 0) ? string.Empty : s.Deliveries[0].Email));

            CreateMap<CustomerHeader, CustomerHeaderViewModel>();
            CreateMap<CustomerDetail, CustomerDetailViewModel>();
            CreateMap<InsuranceCategoryPolicyDashboardCard, InsuranceCategoryPolicyDashboardCardViewModel>();
        }
    }
}
