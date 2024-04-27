using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ISportEventRepository:IRepository<SportEvent>
    {
        IQueryable<SportEvent> GetSportEvents(int insurancePolicyId);
        IQueryable<SportEvent> GetSportEvents(string insurancePolicyCode);
    }
}
