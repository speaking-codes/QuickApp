using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Models
{
    public class CustomerBaseTemplateBuilder
    {
        private CustomerBaseTemplate _customerBaseTemplate;

        public CustomerBaseTemplateBuilder()
        {
            _customerBaseTemplate= new CustomerBaseTemplate();
        }

        public CustomerBaseTemplateBuilder SetJobContratType()
        {
            _customerBaseTemplate.JobContractType = new List<byte>();
            for (var i = 0; i < 12; i++)
                _customerBaseTemplate.JobContractType.Add((byte)i);

            return this;
        }

        public CustomerBaseTemplate Build() => _customerBaseTemplate;
    }
}
