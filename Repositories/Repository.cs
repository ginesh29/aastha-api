using AASTHA2.Common.Helpers;
using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace AASTHA2.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected AASTHA2Context AASTHA2Context { get; set; }
        public DbSet<T> _dbSet;
        public RepositoryBase(AASTHA2Context AASTHA2Context)
        {
            this.AASTHA2Context = AASTHA2Context;
            _dbSet = AASTHA2Context.Set<T>();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, string filter = "", string includeProperties = "", string order = "")
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var include in includeProperties.Split(","))
                    query = query.Include(include);
            }
            if (predicate != null)
                query = query.Where(predicate);

            if (!string.IsNullOrEmpty(filter) && filter != "0")
            {
                DynamicLinqHelper.DynamicSearchQuery(filter, out string dynamicQuery, out object[] param);
                query = query.Where(dynamicQuery, param);
            }
            if (!string.IsNullOrEmpty(order))
                query = query.OrderBy(order);
            return query;
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate, string filter = "", string includeProperties = "")
        {
            return Find(predicate,filter, includeProperties).FirstOrDefault();
        }
        public void Create(T entity)
        {
            this.AASTHA2Context.Set<T>().Add(entity);
        }
        //public void CreateRange(IEnumerable<T> entities)
        //{
        //    db.Configuration.AutoDetectChangesEnabled = false;
        //    this._AASTHA2Context.Set<T>().AddRange(entities);
        //}
        public void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            var dbEntityEntry = AASTHA2Context.Entry(entity);
            if (updatedProperties.Any())
                foreach (var property in updatedProperties)
                    dbEntityEntry.Property(property).IsModified = true;
            else
                this.AASTHA2Context.Set<T>().Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            this.AASTHA2Context.Set<T>().UpdateRange(entities);
        }
        public void Delete(T entity, bool deletePhysical = false)
        {
            if ((bool)deletePhysical)
                this.AASTHA2Context.Set<T>().Remove(entity);
            else
            {
                var dbEntityEntry = AASTHA2Context.Entry(entity);
                dbEntityEntry.Property("IsDeleted").IsModified = true;
            }
        }
        public void DeleteRange(IEnumerable<T> entities, bool deletePhysical = false)
        {
            if ((bool)deletePhysical)
                this.AASTHA2Context.Set<T>().RemoveRange(entities);
            else
            {
                var dbEntityEntry = AASTHA2Context.Entry(entities);
                dbEntityEntry.Property("IsDeleted").IsModified = true;
            }
        }
        public IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters).ToList();
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
