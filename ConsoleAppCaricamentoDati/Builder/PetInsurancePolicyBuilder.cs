using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Builder
{
    public class PetInsurancePolicyBuilder : InsurancePolicyBuilder
    {
        public PetInsurancePolicyBuilder(InsurancePolicyCategory insurancePolicyCategory, Customer customer, IUnitOfWork unitOfWork) : 
            base(insurancePolicyCategory, customer, unitOfWork) { }

        protected override InsurancePolicy NewInsurancePolicy() => new PetInsurancePolicy();

        public override InsurancePolicyBuilder SetIsLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = false;
            return this;
        }

        public override InsurancePolicyBuilder SetDetailItem()
        {
            throw new NotImplementedException();
        }
    }
}
