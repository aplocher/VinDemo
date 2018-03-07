using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using VinDemo.Domain.Interfaces;

namespace VinDemo.Repository.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepository<T, int> where T : class, IDeactivatable
    {
        public RepositoryBase(IVinDemoDbContext context)
        {
            Context = context ?? throw new ArgumentNullException("context");
            DbSet = context.Set<T>();
        }

        protected IVinDemoDbContext Context { get; }

        protected IDbSet<T> DbSet { get; }

        public virtual void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            DbSet.Add(item);
        }

        public virtual void Delete(T item, bool permanent = false)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (permanent)
            {
                DbSet.Attach(item);
                DbSet.Remove(item);
            }
            else
            {
                item.DeactivatedDate = DateTime.UtcNow;
            }
        }

        public virtual IQueryable<T> GetAll(bool includeDeactivated = false)
        {
            return DbSet.Where(x =>
                includeDeactivated || x.DeactivatedDate == null || x.DeactivatedDate > DateTime.UtcNow);
        }

        public virtual IQueryable<T> GetByCriteria(Expression<Func<T, bool>> predicate, bool includeDeactivated = false, bool detach = false)
        {
            var result = GetAll(includeDeactivated).Where(predicate);
            if (detach)
                Context.Entry(result).State = EntityState.Detached;
            return result;
        }

        public virtual T GetById(int id, bool includeDeactivated = false)
        {
            var entity = DbSet.Find(id);
            return
                entity != null && (includeDeactivated || entity.DeactivatedDate == null ||
                                   entity.DeactivatedDate > DateTime.UtcNow)
                    ? entity
                    : null;
        }

        public virtual void Update(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            DbSet.Attach(item);
            Context.Entry(item).State = EntityState.Modified;
        }
    }
}