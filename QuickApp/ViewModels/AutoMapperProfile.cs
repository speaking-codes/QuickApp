// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using AutoMapper;
using DAL.Core;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using Models.Enums;
using Models.ViewModels;
using System.Linq;

namespace QuickApp.ViewModels
{
    public class AutoMapperProfile : Profile
    {
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
                .ForMember(d => d.Gender, map => map.MapFrom(s => s.Gender.GetName()))
                .ReverseMap();

            CreateMap<Address, AddressViewModel>()
                .ReverseMap();

            CreateMap<Delivery, DeliveryViewModel>()
                .ReverseMap();

            CreateMap<Customer, CustomerGridViewModel>()
                .ForMember(d => d.FullName, map => map.MapFrom(s => $"{s.LastName} {s.FirstName}"))
                .ForMember(d => d.BirthDate, map => map.MapFrom(s => s.BirthDate.ToString("dd MMM yyyy")))
                .ForMember(d => d.Gender, map => map.MapFrom(s => s.Gender.GetName()))
                .ForMember(d => d.Residence, map => map.MapFrom(s => (s.Addresses == null || s.Addresses.Count == 0) ? string.Empty : $"{s.Addresses[0].City} ({s.Addresses[0].Province})"));

            CreateMap<Customer, CustomerDetailHeaderViewModel>()
                .ForMember(d => d.FullName, map => map.MapFrom(s => $"{s.LastName} {s.FirstName}"))
                .ForMember(d => d.Residence, map => map.MapFrom(s => (s.Addresses == null || s.Addresses.Count == 0) ? string.Empty : $"{s.Addresses[0].Location}, {s.Addresses[0].HouseNumber} - {s.Addresses[0].City} ({s.Addresses[0].Province})"))
                .ForMember(d => d.PhoneNumber, map => map.MapFrom(s => (s.Deliveries == null|| s.Deliveries.Count == 0) ? string.Empty : s.Deliveries[0].PhoneNumber))
                .ForMember(d => d.Email, map => map.MapFrom(s => (s.Deliveries == null || s.Deliveries.Count == 0) ? string.Empty : s.Deliveries[0].Email));
        }
    }
}
