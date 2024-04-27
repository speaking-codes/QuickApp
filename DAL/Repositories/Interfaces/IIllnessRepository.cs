using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IIllnessRepository : IRepository<Illness>
    {
        IQueryable<Illness> GetIllnesses(int insurancePolicyId);
        IQueryable<Illness> GetIllnesses(string insurancePolicyCode);
    }
}
