using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class FamilyTypeRepository: Repository<FamilyType>, IFamilyTypeRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public FamilyTypeRepository(ApplicationDbContext context) : base(context) { }
    }
}
