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
    public class ConfigurationModelRepository : Repository<ConfigurationModel>, IConfigurationModelRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public ConfigurationModelRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<ConfigurationModel> GetConfigurationModels() =>
                _appContext.ConfigurationModels
                           .Include(x => x.Model)
                                .ThenInclude(x => x.Brand)
                                    .ThenInclude(x => x.BrandType)
                           .Include(x => x.ModelType)
                           .Where(x => x.IsActive);

        public IQueryable<ConfigurationModel> GetCarConfigurationModels() =>
                _appContext.ConfigurationModels
                           .Include(x => x.Model)
                                .ThenInclude(x => x.Brand)
                                    .ThenInclude(x => x.BrandType)
                           .Include(x => x.ModelType)
                           .Where(x => !x.ModelType.IsByke && !x.Model.Brand.BrandType.IsByke && x.IsActive);

        public IQueryable<ConfigurationModel> GetBykeConfigurationModels()=>
            _appContext.ConfigurationModels
                       .Include(x => x.Model)
                                .ThenInclude(x => x.Brand)
                                    .ThenInclude(x => x.BrandType)
                           .Include(x => x.ModelType)
                       .Where(x => x.ModelType.IsByke && x.Model.Brand.BrandType.IsByke && x.IsActive);

        public IQueryable<ConfigurationModel> GetConfigurationsByInsurancePolicyVehicle(int idPolicy) =>
            _appContext.ConfigurationModels
                       .Include(x => x.Model)
                       .ThenInclude(x => x.Brand)
                       .Where(x => x.Vehicles.Any(y => y.InsurancePolicy.Id == idPolicy));
    }
}
