using DAL.BuilderModelTemplate;
using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exstensions
{
    public static class BusinessExstension
    {
        public static Business SetBusinessTitle(this Business business, Random random, IList<string> businessTitles)
        {
            var index = random.Next(businessTitles.Count);
            business.BusinessTitle = businessTitles[index];
            return business;
        }
        public static Business SetBusinessLocation(this Business business, Random random, AdressModelTemplate addressTemplate)
        {
            business.BusinessLocation = Utilities.GenerateFullStreetName(addressTemplate.StreetTypes, addressTemplate.StreetNames, random);
            return business;
        }
        public static Business SetBusinessType(this Business business, Random random, IList<BusinessType> businessTypes)
        {
            var index = random.Next(businessTypes.Count);
            business.BusinessType = businessTypes[index];
            return business;
        }
        public static Business SetEmployeeNumbers(this Business business, Random random)
        {
            business.EmployeeNumbers = random.Next(business.BusinessType.MinEmployedNumbers, business.BusinessType.MaxEmployedNumbers);
            return business;
        }
        public static Business SetAnnualRevenue(this Business business, Random random)
        {
            business.AnnualRevenue = business.BusinessType.MinGrossAnnualRevenue * (1 + random.NextSingle());
            return business;
        }
        public static Business SetMunicipality(this Business business, Random random, IList<Municipality> municipalities)
        {
            var index = random.Next(municipalities.Count);
            business.Municipality = municipalities[index];
            return business;
        }
        public static string GetBusinessDescription(this Business business)
        {
            var sb = new StringBuilder(string.Empty);
            sb.Append($"Nome attività: {business.BusinessTitle}");
            sb.Append($" - presso: {business.BusinessLocation}");
            sb.Append($" - Nel comune: {business.Municipality.PostalCode} {business.Municipality.MunicipalityName} ({business.Municipality.Province.ProvinceAbbreviation})");
            sb.Append($" - Numero dipedenti: {business.EmployeeNumbers}");
            sb.Append($" - Fatturato annuo: {business.AnnualRevenue} €");
            return sb.ToString();
        }
        public static IList<string> GetBusinessDescriptions(this IEnumerable<Business> businesses)
        {
            var businessDescriptions = new List<string>();
            foreach (var item in businesses)
                businessDescriptions.Add(item.GetBusinessDescription());
            return businessDescriptions;
        }
    }
}
