using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ContractTypeRepository : Repository<ContractType>, IContractTypeRepository
    {
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public ContractTypeRepository(ApplicationDbContext context) : base(context) { }

        public override void Add(ContractType entity)=> base.Add(entity);

        public override void AddRange(IEnumerable<ContractType> entities)=>base.AddRange(entities);

        public override void Update(ContractType entity)=> base.Update(entity);

        public override void UpdateRange(IEnumerable<ContractType> entities)=>base.UpdateRange(entities);

        public override void Remove(ContractType entity)=> base.Remove(entity);

        public override void RemoveRange(IEnumerable<ContractType> entities)=>base.RemoveRange(entities);

        public override int Count()=> base.Count();

        public override IList<ContractType> Find(Expression<Func<ContractType, bool>> predicate)=>base.Find(predicate);

        public override ContractType GetSingleOrDefault(Expression<Func<ContractType, bool>> predicate)=>base.GetSingleOrDefault(predicate);

        public override ContractType Get(int id)=>base.Get(id);

        public override IList<ContractType> GetAll()=>base.GetAll();
    }
}
