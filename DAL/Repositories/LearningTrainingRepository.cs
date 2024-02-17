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
    public class LearningTrainingRepository : Repository<LearningTraining>, ILearningTrainingRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public LearningTrainingRepository(DbContext context) : base(context)
        {
        }
    }
}
