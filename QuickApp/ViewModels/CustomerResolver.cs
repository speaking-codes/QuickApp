using AutoMapper;
using Models.Entities;
using Models.Enums;
using System.Linq;

namespace QuickApp.ViewModels
{
    public class CustomerFullNameResolver:IValueResolver<Customer,CustomerGridViewModel, string>
    {
        public string Resolve(Customer source, CustomerGridViewModel destination, string destMember, ResolutionContext context)
        {
            return $"{source.LastName} {source.FirstName}";
        }
    }

    public class CustomerBirthDateResolver:IValueResolver<Customer, CustomerGridViewModel, string>
    {
        public string Resolve(Customer source, CustomerGridViewModel destination, string destMember, ResolutionContext context)
        {
            return source.BirthDate.ToString("dd/MMM/yyyy");
        }
    }

    public class CustomerGenderResolver : IValueResolver<Customer, CustomerGridViewModel, string>
    {
        public string Resolve(Customer source, CustomerGridViewModel destination, string destMember, ResolutionContext context)
        {
            return source.Gender.GetName();
        }
    }

    public class CustomerResidenceResolver : IValueResolver<Customer, CustomerGridViewModel, string>
    {
        public string Resolve(Customer source, CustomerGridViewModel destination, string destMember, ResolutionContext context)
        {
            if (source.Addresses == null || source.Addresses.Count == 0)
                return string.Empty;

            return $"{source.Addresses.First().City} ({source.Addresses.First().Province})";
        }
    }
}