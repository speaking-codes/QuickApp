using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ILegalProtectionRepository : IRepository<LegalProtection>
    {
        IQueryable<LegalProtection> GetLegalProtections(int insurancePolicyId);
        IQueryable<LegalProtection> GetLegalProtections(string insurancePolicyCode);
    }
}
