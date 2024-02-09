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
        public IList<string> ProviderMailTemplates { get; set; }
        public IList<double> Incomes { get; set; }
    }

    public class FirstNameTemplate
    {
        public string FirstName { get; set; }
        public bool IsMan { get; set; }
    }
}
