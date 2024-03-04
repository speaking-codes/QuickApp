using DAL.BuilderModelTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelFactory.Interfaces
{
    public interface ITemplateFactory : IDisposable
    {
        AdressModelTemplate CreateAddressModelTemplate();
        CustomerModelTemplate CreateCustomerModelTemplate();
    }
}
