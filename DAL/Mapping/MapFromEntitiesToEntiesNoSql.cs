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

            return customer.Deliveries.FirstOrDefault(x => x.IsPrimary);
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

            var address = customer.Addresses.FirstOrDefault(x => x.IsPrimary);
            if (address == null) return string.Empty;

            return $"{address.Location}; {address.Municipality.MunicipalityName} ({address.Municipality.Province.ProvinceAbbreviation})";
        }

        private static string getCityDetail(this Customer customer)
        {
            return $"{customer.BirthMunicipality.PostalCode} - {customer.BirthMunicipality.MunicipalityName} ({customer.BirthMunicipality.Province.ProvinceAbbreviation})";
        }

        private static AddressDetail getAddressDetail(this Address address)
        {
            return new AddressDetail
            {
                AddressType = address.AddressType.GetDefinition(),
                City = address.Municipality.MunicipalityName,
                Country = address.Municipality.Province.ProvinceAbbreviation,
                PostalCode = address.Municipality.PostalCode,
                Location = address.Location,
                IsPrimary = address.IsPrimary
            };
        }

        private static IList<AddressDetail> getAddressDetails(this Customer customer)
        {
            if (customer == null)
                return new List<AddressDetail>();

            if (customer.Addresses == null || customer.Addresses.Count == 0)
                return new List<AddressDetail>();

            var addressDetails = new List<AddressDetail>();
            foreach (var item in customer.Addresses)
                addressDetails.Add(item.getAddressDetail());

            return addressDetails;
        }

        private static DeliveryDetail getDeliveryDetail(this Delivery delivery)
        {
            return new DeliveryDetail
            {
                DeliveryType = delivery.DeliveryType.GetDefinition(),
                Email = delivery.Email,
                Phone = delivery.PhoneNumber,
                IsPrimary = delivery.IsPrimary
            };
        }

        private static IList<DeliveryDetail> getDeliveryDetails(this Customer customer)
        {
            if (customer == null)
                return new List<DeliveryDetail>();

            if (customer.Deliveries == null || customer.Deliveries.Count == 0)
                return new List<DeliveryDetail>();

            var deliveryDetails = new List<DeliveryDetail>();
            foreach (var item in customer.Deliveries)
                deliveryDetails.Add(item.getDeliveryDetail());

            return deliveryDetails;
        }

        private static SalesLineChart ToSalesLineChart(this Tuple<SalesLineType, IList<InsurancePolicy>> customerInsuranceCoverage)
        {
            return new SalesLineChart
            {
                SalesLineCode = customerInsuranceCoverage.Item1.SalesLineCode,
                SalesLineName = customerInsuranceCoverage.Item1.SalesLineName,
                BackGroundColor = customerInsuranceCoverage.Item1.BackGroundColor,
                TotalCount = customerInsuranceCoverage.Item2.Count,
                TotalPrice = customerInsuranceCoverage.Item2.Sum(x => x.TotalPrize)
            };
        }

        public static CustomerHeader ToNoSqlHeaderEntity(this Customer value)
        {
            return new CustomerHeader
            {
                CustomerCode = value.CustomerCode,
                FullName = $"{value.LastName} {value.FirstName}",
                Address = getAddressHeader(value),
                Email = getEmail(value),
                Phone = getPhone(value),
            };
        }

        public static CustomerDetail ToNoSqlDetailEntity(this Customer value)
        {
            return new CustomerDetail
            {
                CustomerCode = value.CustomerCode,

                FullName = $"{value.LastName} {value.FirstName}",
                BirthDate = value.BirthDate.HasValue ? value.BirthDate.Value.ToString("D") : string.Empty,
                BirthPlace = value.getCityDetail(),
                Gender = value.Gender.GetDefinition(),

                MaritalStatus = value.MaritalStatus != null ? value.MaritalStatus.MaritalStatusDescription : string.Empty,
                FamilyDescription = value.FamilyType != null ? value.FamilyType.FamilyTypeDescription : string.Empty,
                ChildrenNumber = value.ChildrenNumber.HasValue ? value.ChildrenNumber.ToString() : string.Empty,

                AddressDetails = value.getAddressDetails(),
                DeliveryDetails = value.getDeliveryDetails(),

                JobTitle = value.ProfessionType != null ? value.ProfessionType.ProfessionTypeDescription : string.Empty,
                IsFrelancer = value.ProfessionType != null ? value.ProfessionType.IsFreelancer : false,
                ContractTitle = value.ContractType != null ? value.ContractType.ContractTypeTitle : string.Empty,
                Income = value.Income.HasValue ? "€ " + value.Income.Value.ToString("C0") : string.Empty
            };
        }

        public static InsuranceCoverageChart ToInsuranceCoverageChart(this IList<Tuple<SalesLineType, IList<InsurancePolicy>>> customerInsuranceCoverages, string customerCode)
        {
            var insuranceCoverageChart = new InsuranceCoverageChart();
            insuranceCoverageChart.CustomerCode = customerCode;
            insuranceCoverageChart.SalesLineCharts = new List<SalesLineChart>();
            foreach(var item in customerInsuranceCoverages)
                insuranceCoverageChart.SalesLineCharts.Add(item.ToSalesLineChart());

            return insuranceCoverageChart;
        }
    }
}
