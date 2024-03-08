using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Models;
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
        InsurancePolicyTemplate CreateInsurancePolicyTemplate();
        IList<IInsurancePolicyBuilder> CreateInsurancePolicyBuilders(int count, IList<InsurancePolicyCategory> insurancePolicyCategories, Random random);
    }
}
