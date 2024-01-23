using DAL.Enums;
using DAL.Models;
using DAL.ModelsNoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mapping
{
    public static class MapFromEntitiesToEntiesNoSql
    {
        private static Delivery getDelivery(Customer customer)
        {
            if (customer == null) return null;

            if (customer.Deliveries == null || customer.Deliveries.Count == 0) return null;

            return customer.Deliveries.FirstOrDefault(x => x.DeliveryType == Enums.EnumDeliveryType.Privato);
        }

        private static string getPhone(Customer customer)
        {
            var delivery = getDelivery(customer);

            if (delivery == null) return string.Empty;

            return delivery.PhoneNumber;
        }

        private static string getEmail(Customer customer)
        {
            var delivery = getDelivery(customer);

            if (delivery == null) return string.Empty;

            return delivery.Email;
        }

        private static string getAddressHeader(Customer customer)
        {
            if (customer == null) return string.Empty;

            if (customer.Addresses == null || customer.Addresses.Count == 0) return string.Empty;

            var address = customer.Addresses.FirstOrDefault(x => x.AddressType == Enums.EnumAddressType.Residenza);
            if (address == null) return string.Empty;

            return $"{address.Location}; {address.Municipality.MunicipalityName} ({address.Municipality.Province.ProvinceAbbreviation})";
        }

        private static string getAddressDetail(Customer customer)
        {
            if (customer == null) return string.Empty;

            if (customer.Addresses == null || customer.Addresses.Count == 0) return string.Empty;

            var address = customer.Addresses.FirstOrDefault(x => x.AddressType == Enums.EnumAddressType.Residenza);
            if (address == null) return string.Empty;

            return address.Location;
        }

        private static string getAddressCity(Customer customer)
        {
            if (customer == null) return string.Empty;

            if (customer.Addresses == null || customer.Addresses.Count == 0) return string.Empty;

            var address = customer.Addresses.FirstOrDefault(x => x.AddressType == Enums.EnumAddressType.Residenza);
            if (address == null) return string.Empty;

            return $"{address.Municipality.PostalCode} - {address.Municipality.MunicipalityName} ({address.Municipality.Province.ProvinceAbbreviation})";
        }

        public static CustomerHeader ToNoSqlHeaderEntity(this Customer value)
        {
            return new CustomerHeader
            {
                CustomerCode = value.CustomerCode,
                CustomerName = $"{value.LastName} {value.FirstName}",
                CustomerAddress = getAddressHeader(value),
                CustomerEmail = getEmail(value),
                CustomerPhone = getPhone(value),
            };
        }

        public static CustomerDetail ToNoSqlDetailEntity(this Customer value)
        {
            return new CustomerDetail
            {
                CustomerCode = value.CustomerCode,

                CustomerName = $"{value.LastName} {value.FirstName}",
                CustomerBirthDate = value.BirthDate.ToString("D"),
                CustomerBirthPlace = getCityDetail(value),
                CustomerGender = value.Gender.GetDefinition(),

                CustomerAddressLocation = getAddressDetail(value),
                CustomerAddressCity = getAddressCity(value),

                CustomerEmail = getEmail(value),
                CustomerPhone = getPhone(value),

                CustomerJobTitle = value.JobTitle,
                CustomerRal = "€ " + value.Ral.ToString()
            };
        }

        private static string getCityDetail(Customer customer)
        {
            return $"{customer.BirthMunicipality.PostalCode} - {customer.BirthMunicipality.MunicipalityName} ({customer.BirthMunicipality.Province.ProvinceAbbreviation})";
        }
    }
}
