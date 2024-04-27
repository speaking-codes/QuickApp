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

        public IInsurancePolicyBuilder SetInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            insurancePolicy ??= new InsurancePolicy();
            InsurancePolicy = insurancePolicy;
            return this;
        }

        public IInsurancePolicyBuilder SetIssueDate()
        {
            InsurancePolicy.IssueDate = DateTime.Now.AddDays(Random.Next(-360, -30));
            return this;
        }

        public IInsurancePolicyBuilder SetExpiryDate()
        {
            InsurancePolicy.ExpiryDate = InsurancePolicy.IssueDate.AddDays(360);
            return this;
        }

        public IInsurancePolicyBuilder SetInsuredMaximum()
        {
            InsurancePolicy.InsuredMaximum = (1 + Random.NextDouble()) * Random.Next(100, 10000000);
            return this;
        }

        public IInsurancePolicyBuilder SetTotalPrize()
        {
            InsurancePolicy.TotalPrize = (1 + Random.NextDouble()) * Random.Next(10, 3000);
            return this;
        }

        public IInsurancePolicyBuilder SetCustomer(Customer customer)
        {
            InsurancePolicy.Customer = customer;
            return this;
        }

        public IInsurancePolicyBuilder SetWarranties()
        {
            InsurancePolicy.WarrantySelecteds = new List<WarrantySelected>();
            InsurancePolicy.WarrantySelecteds.Add(new WarrantySelected() { WarrantyAvaible = InsurancePolicy.InsurancePolicyCategory.WarrantyAvaibles.Where(x => x.IsPrimary).FirstOrDefault() });

            var warrantyAvaibles = InsurancePolicy.InsurancePolicyCategory.WarrantyAvaibles.Where(x => !x.IsPrimary).ToList();
            var maxWarrantySelected = Random.Next(0, warrantyAvaibles.Count);
            while (InsurancePolicy.WarrantySelecteds.Count < maxWarrantySelected)
            {
                var index = Random.Next(0, maxWarrantySelected);
                if (!InsurancePolicy.WarrantySelecteds.Any(x => x.WarrantyAvaible.Id == warrantyAvaibles[index].Id))
                    InsurancePolicy.WarrantySelecteds.Add(new WarrantySelected { WarrantyAvaible = warrantyAvaibles[index] });
            }
            return this;
        }

        public virtual IInsurancePolicyBuilder SetLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = InsurancePolicy.TotalPrize > 1800 && InsurancePolicy.InsuredMaximum > 1000000;
            return this;
        }

        public abstract IInsurancePolicyBuilder SetInsurancePolicyCategory(IList<InsurancePolicyCategory> insurancePolicyCategories);

        public abstract IInsurancePolicyBuilder SetDetailItems(InsurancePolicyTemplate insurancePolicyTemplate);

        public virtual InsurancePolicy Build() => InsurancePolicy;
    }
}
