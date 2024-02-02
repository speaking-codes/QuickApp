using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IRepositoryMunicipality : IRepository<Municipality>
    {
        IQueryable<Municipality> GetMunicipalities();
        IQueryable<Municipality> GetMunicipality(int municipalityId);        
    }
}
