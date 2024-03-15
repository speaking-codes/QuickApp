using DAL.BuilderModel.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class BaggageLossInsurancePolicyBuilder:InsurancePolicyBuilder, IBaggageLossInsurancePolicyBuilder
    {
        public override IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories)
        {
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategories.Where(x => x.Id == (byte)EnumInsurancePolicyCategory.PerditaBagaglio).FirstOrDefault();
            return this;
        }
    }
}
