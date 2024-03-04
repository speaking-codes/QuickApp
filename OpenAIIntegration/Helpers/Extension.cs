using DAL.Enums;
using DAL.Models;
using OpenAIIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIIntegration.Helpers
{
    public static class Extension
    {
        public static Customer ToModel(this CustomerApi customerApi, Customer customer)
        {
            return new Customer
            {
                FirstName = customerApi.FirstName,
                LastName = customerApi.LastName,
                BirthDate = customerApi.BirthDate,
                ChildrenNumber = customerApi.ChildrenNumber,
                Income = customerApi.Income
            };
        }      
    }
}
