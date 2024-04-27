using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IVehicleRepository:IRepository<Vehicle>
    {
        IQueryable<Vehicle> GetVehicles(int insurancePolicyId);
        IQueryable<Vehicle> GetVehicles(string insurancePolicyCode);
    }
}
