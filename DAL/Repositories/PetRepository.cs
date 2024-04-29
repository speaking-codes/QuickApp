using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PetRepository : Repository<Pet>, IPetRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PetRepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<Pet> GetPets(int insurancePolicyId) =>
            _appContext.Pets
                       .Include(p => p.BreedPetDetailType)
                            .ThenInclude(x => x.BreedPetType)
                                .ThenInclude(y => y.PetType)
                       .Where(p => p.InsurancePolicy.Id == insurancePolicyId);

        public IQueryable<Pet> GetPets(string insurancePolicyCode)=>
            _appContext.Pets
                       .Include(p => p.BreedPetDetailType)
                            .ThenInclude(x => x.BreedPetType)
                                .ThenInclude(y => y.PetType)
                       .Where(p=>p.InsurancePolicy.InsurancePolicyCode== insurancePolicyCode);
    }
}
