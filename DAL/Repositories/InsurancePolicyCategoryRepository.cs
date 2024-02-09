﻿using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class InsurancePolicyCategoryRepository : Repository<InsurancePolicyCategoryRepository>, IInsurancePolicyCategoryRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public InsurancePolicyCategoryRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategories() =>
            _appContext.InsurancePolicyCategories
                       .Include(x => x.SalesLine);

        public IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategory(string insurancePolicyCategoryCode) =>
            _appContext.InsurancePolicyCategories
                       .Include(x => x.SalesLine)
                       .Where(x => x.InsurancePolicyCategoryCode == insurancePolicyCategoryCode);

        public IQueryable<InsurancePolicyCategory> GetInsurancePolicyCategoryStatistics(int year) =>
            _appContext.InsurancePolicyCategories
                       .Include(x => x.SalesLine)
                       .Include(x => x.InsurancePolicyCategoryStatics)
                       .Where(x => x.InsurancePolicyCategoryStatics.Any(y => y.Year == year));
    }
}
