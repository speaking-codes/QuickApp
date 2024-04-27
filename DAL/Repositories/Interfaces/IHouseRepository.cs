using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IHouseRepository : IRepository<House>
    {
        IQueryable<House> GetHouses(int insurancePolicyId);
        IQueryable<House> GetHouses(string insurancePolicyCode);
    }
}
