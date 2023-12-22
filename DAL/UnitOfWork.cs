// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using DAL.Repositories;
using DAL.Repositories.Interfaces;
using System;
using System.Linq;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ICustomerRepository _customers;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers
        {
            get
            {
                _customers ??= new CustomerRepository(_context);

                return _customers;
            }
        }       

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
