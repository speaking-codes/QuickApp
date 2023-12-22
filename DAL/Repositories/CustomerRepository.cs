﻿// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Linq;

namespace DAL.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        { }

        public IQueryable<Customer> GetTopActiveCustomers(int count)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _appContext.Customers
                .AsSingleQuery()
                .OrderBy(c => c.FullName);
        }

        public IQueryable<Customer> GetCustomer(string taxIdCode)
        {
            return _appContext.Customers
                            .Include(c => c.Deliveries)
                            .Include(c => c.Addresses)
                            .AsSingleQuery()
                            .Where(c => c.TaxIdCode == taxIdCode);
        }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;
    }
}
