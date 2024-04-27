using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IInjuryRepository : IRepository<Injury>
    {
        IQueryable<Injury> GetInjuries(int insurancePolicyId);
        IQueryable<Injury> GetInjuries(string insurancePolicyCode);
    }
}
