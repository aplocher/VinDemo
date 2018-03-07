using System;
using System.Linq;
using System.Linq.Expressions;

namespace VinDemo.Domain.Interfaces
{
    public interface IRepository<TEntity, in TId> where TEntity : class, IDeactivatable
    {
        IQueryable<TEntity> GetAll(bool includeDeactivated = false);
        IQueryable<TEntity> GetByCriteria(Expression<Func<TEntity, bool>> predicate, bool includeDeactivated = false, bool detach = false);
        TEntity GetById(TId id, bool includeDeactivated = false);
        void Add(TEntity item);
        void Delete(TEntity item, bool permanent = false);
        void Update(TEntity item);
    }
}