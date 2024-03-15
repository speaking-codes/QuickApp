using DAL;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Builder
{
    public abstract class InsurancePolicyBuilder
    {
        protected Random Random { get; private set; }
        protected InsurancePolicy InsurancePolicy { get; set; }
        protected IUnitOfWork UnitOfWork { get; private set; }

        public InsurancePolicyBuilder(InsurancePolicyCategory insurancePolicyCategory, Customer customer, IUnitOfWork unitOfWork)
        {
            InsurancePolicy = new InsurancePolicy();
            InsurancePolicy.InsurancePolicyCategory = insurancePolicyCategory;
            InsurancePolicy.Customer = customer;
            Random = new Random();
            UnitOfWork = unitOfWork;
        }

        protected abstract InsurancePolicy NewInsurancePolicy();

        public InsurancePolicyBuilder SetIssueDate()
        {
            InsurancePolicy.IssueDate = DateTime.Now.AddDays(Random.Next(-300, -30));
            return this;
        }
        public InsurancePolicyBuilder SetExpireDate()
        {
            InsurancePolicy.ExpiryDate = InsurancePolicy.IssueDate.AddDays(360);
            return this;
        }

        public abstract InsurancePolicyBuilder SetIsLuxuryPolicy();
        public abstract InsurancePolicyBuilder SetDetailItem();

        public InsurancePolicyBuilder SetInsuredMaximum()
        {
            InsurancePolicy.InsuredMaximum = Random.NextDouble() * Random.Next(100, 100000);
            return this;
        }

        public InsurancePolicyBuilder SetTotalPrize()
        {
            InsurancePolicy.TotalPrize = Random.NextDouble() * Random.Next(10, 1000);
            return this;
        }

        public InsurancePolicy Build() => InsurancePolicy;
    }
}
