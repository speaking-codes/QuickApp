using DAL.Enums;
using DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL.Exstensions
{
    public static class CustomerExstension
    {
        public static string ToCsv(this Customer customer, InsurancePolicyCategory insurancePolicyCategory)
        {
            var sb = new StringBuilder();
            sb.Append($"{customer.Gender.GetCode()};");
            sb.Append($"{customer.BirthDate.ToString("dd/MM/yyyy")};");

            if (customer.MaritalStatus != null)
                sb.Append($"{customer.MaritalStatus.MaritalStatusDescription};false;");
            else
                sb.Append(";false;");

            sb.Append($"{customer.ChildrenNumber};0;");

            if (customer.ProfessionType != null)
                sb.Append($"{customer.ProfessionType.ProfessionTypeDescription};");
            else
                sb.Append(";");

            sb.Append($"{customer.Income};");

            if (customer.IncomeType != null)
                sb.Append($"{customer.IncomeType.IncomeTypeDescription};");
            else
                sb.Append(";");

            if (customer.ContractType != null)
                sb.Append($"{customer.ContractType.ContractTypeTitle};");
            else
                sb.Append(";");

            var address = customer.Addresses.Where(x => x.IsPrimary).FirstOrDefault();
            if (address != null)
                sb.Append($"{address.Municipality.Province.ProvinceAbbreviation};");
            else
                sb.Append(";");

            if (insurancePolicyCategory != null)
                sb.Append($"{insurancePolicyCategory.InsurancePolicyCategoryName}");

            return sb.ToString();
        }
    }
}
