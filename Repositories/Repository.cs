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
        protected AASTHAContext _AASTHAContext { get; set; }
        public DbSet<T> _dbSet;
        public RepositoryBase(AASTHAContext AASTHAContext)
        {
            _AASTHAContext = AASTHAContext;
            _dbSet = AASTHAContext.Set<T>();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> filter, string search, bool ShowDeleted, out int totalCount, string order = "", int skip = 0, int take = 0, params Expression<Func<T, object>>[] includeProperty)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperty != null)
            {
                query = includeProperty.Aggregate(query, (current, include) => current.Include(include));
            }

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(search))
            {
                string dynamicQuery;
                object[] param;
                DynamicLinqHelper.DynamicSearchQuery(search, out dynamicQuery, out param);
                query = query.Where(dynamicQuery, param);
            }

            if (ShowDeleted)
                query = query.Where("IsDeleted=true");
            else if (!ShowDeleted)
                query = query.Where("IsDeleted=false or IsDeleted=null");

            if (!string.IsNullOrEmpty(order))
                query = query.OrderBy(order);
            else
                query = query.OrderBy("Id asc");

            totalCount = query.Count();

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);
            //else
            //    query = query.Take(15);
            return query;
        }
        public T FirstOrDefault(Expression<Func<T, bool>> filter, string search = "", bool ShowDeleted = false, params Expression<Func<T, object>>[] includeProperty)
        {
            int totalCount;
            return Find(filter, search, ShowDeleted, out totalCount,"",0,0, includeProperty).FirstOrDefault();
        }
        public void Create(T entity)
        {
            this._AASTHAContext.Set<T>().Add(entity);
        }
        public void CreateRange(IEnumerable<T> entities)
        {
            this._AASTHAContext.Set<T>().AddRange(entities);
        }
        public void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            var dbEntityEntry = _AASTHAContext.Entry(entity);
            if (updatedProperties.Any())
                foreach (var property in updatedProperties)
                    dbEntityEntry.Property(property).IsModified = true;
            else
                this._AASTHAContext.Set<T>().Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {
            this._AASTHAContext.Set<T>().UpdateRange(entities);
        }
        public void Delete(T entity, bool deletePhysical = false)
        {
            if ((bool)deletePhysical)
                this._AASTHAContext.Set<T>().Remove(entity);
            else
            {
                var dbEntityEntry = _AASTHAContext.Entry(entity);
                dbEntityEntry.Property("IsDeleted").CurrentValue = true;
                dbEntityEntry.Property("IsDeleted").IsModified = true;
            }
        }
        public void DeleteRange(IEnumerable<T> entities, bool deletePhysical = false)
        {
            if ((bool)deletePhysical)
                this._AASTHAContext.Set<T>().RemoveRange(entities);
            else
            {
                var dbEntityEntry = _AASTHAContext.Entry(entities);
                dbEntityEntry.Property("IsDeleted").CurrentValue = true;
                dbEntityEntry.Property("IsDeleted").IsModified = true;
            }
        }
        public IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbSet.FromSql(query, parameters).ToList();
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
