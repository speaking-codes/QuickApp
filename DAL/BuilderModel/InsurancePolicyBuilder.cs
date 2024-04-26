using DAL.BuilderModel.Interfaces;
using DAL.BuilderModelTemplate;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public abstract class InsurancePolicyBuilder : IInsurancePolicyBuilder
    {
        protected virtual InsurancePolicy InsurancePolicy { get; private set; }
        protected readonly Random Random;

        public InsurancePolicyBuilder()
        {
            Random = new Random();
        }

        public virtual IInsurancePolicyBuilder SetInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            insurancePolicy ??= new InsurancePolicy();
            InsurancePolicy = insurancePolicy;
            return this;
        }

        public virtual IInsurancePolicyBuilder SetIssueDate()
        {
            InsurancePolicy.IssueDate = DateTime.Now.AddDays(Random.Next(-360, -30));
            return this;
        }

        public virtual IInsurancePolicyBuilder SetExpiryDate()
        {
            InsurancePolicy.ExpiryDate = InsurancePolicy.IssueDate.AddDays(Random.Next(10, 180));
            return this;
        }

        public virtual IInsurancePolicyBuilder SetInsuredMaximum()
        {
            InsurancePolicy.InsuredMaximum = (1 + Random.NextDouble()) * Random.Next(100, 10000000);
            return this;
        }

        public virtual IInsurancePolicyBuilder SetTotalPrize()
        {
            InsurancePolicy.TotalPrize = (1 + Random.NextDouble()) * Random.Next(10, 3000);
            return this;
        }

        public virtual IInsurancePolicyBuilder SetLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = InsurancePolicy.TotalPrize > 1800 && InsurancePolicy.InsuredMaximum > 1000000;
            return this;
        }

        public abstract IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories);

        public virtual IInsurancePolicyBuilder SetCustomer(Customer customer)
        {
            InsurancePolicy.Customer = customer;
            return this;
        }

        public abstract IInsurancePolicyBuilder SetWarranties();

        public virtual IInsurancePolicyBuilder SetDetailItem(InsurancePolicyTemplate insurancePolicyTemplate) => this;

        public virtual InsurancePolicy Build() => InsurancePolicy;
    }
}
