using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Models
{
    public class CustomerBaseTemplate
    {
        public IList<string> LastNames { get; set; }
        public IList<FirstNameTemplate> FirstNameTemplates { get; set; }
        public IList<string> AddressTemplates { get; set; }
        //public IList<CityTemplate> CityTemplates { get; set; }
        public IList<byte> JobContractType { get; set; }
        public IList<JobTemplate> JobTemplates { get; set; }
        public IList<string> ProviderMailTemplates { get; set; }
    }

    public class FirstNameTemplate
    {
        public string FirstName { get; set; }
        public bool IsMan { get; set; }
    }

    public class CityTemplate
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string County { get; set; }
    }

    public class JobTemplate
    {
        public string JobTitle { get; set; }
        public double Ral { get; set; }
    }
}
