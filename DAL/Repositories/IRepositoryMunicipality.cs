using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IRepositoryMunicipality:IRepository<Municipality>
    {
        Task<IList<Municipality>> GetMunicipalitiesAsync();
        IList<Municipality> GetMunicipalities();
        Municipality GetMunicipality(int municipalityId);
        Task<Municipality> GetMunicipalityAsync(int municipalityId);
    }
}
