using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace AASTHA2.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected AASTHAContext _AASTHAContext { get; set; }
        public DbSet<T> _dbSet;
        public RepositoryBase(AASTHAContext AASTHAContext)
        {
            _AASTHAContext = AASTHAContext;
            _dbSet = AASTHAContext.Set<T>();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> filter = null, string order = null, int skip = 0, int take = 0)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(order))
                query = query.OrderBy(order);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            return query;
        }
        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string order = null, int skip = 0, int take = 0)
        {
            return Find(filter, order, skip, take).FirstOrDefault();
        }
        public bool IsExist(Expression<Func<T, bool>> filter = null, string order = null, int skip = 0, int take = 0)
        {
            return Find(filter, order, skip, take).Any();
        }
        public void Create(T entity)
        {
            this._AASTHAContext.Set<T>().Add(entity);
        }
        public void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {

            //var entry = _AASTHAContext.Entry(entity);
            //this._AASTHAContext.Set<T>().Attach(entity);
            ////var updatedFields = entry.Properties.Where(m => m.IsModified == true).ToList();
            ////updatedFields.ForEach(m => m.IsModified = false);
            //var a = entry.Property("Username").IsModified;
            //var b = entry.Property("Password").IsModified;
            //var c = entry.Property("IsDeleted").IsModified;
            //foreach (var propertyName in updatedProperties)
            //    entry.Property(propertyName).IsModified = true;
            //a = entry.Property("Username").IsModified;
            //b = entry.Property("Password").IsModified;
            //c = entry.Property("IsDeleted").IsModified;
            // var updatedFields = entry.Properties.Where(m => m.IsModified == true);

        }
        public void Delete(T entity)
        {
            this._AASTHAContext.Set<T>().Remove(entity);
        }
        //public async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        //{
        //    IQueryable<T> query = _dbSet;

        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    foreach (var includeProperty in includeProperties.Split
        //        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    return await query.ToListAsync();
        //}
        //public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        //{
        //    return await Find(filter).FirstOrDefaultAsync();
        //}
    }
}
