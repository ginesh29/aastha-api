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

        public IQueryable<T> Find(Expression<Func<T, bool>> filter, string search, bool ShowDeleted, out int totalCount, string order = "", int skip = 0, int take = 15, params Expression<Func<T, object>>[] includePropery)
        {
            IQueryable<T> query = _dbSet;

            if (includePropery != null)
            {
                query = includePropery.Aggregate(query, (current, include) => current.Include(include));
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

            if (Convert.ToBoolean(ShowDeleted))
                query = query.Where("IsDeleted!=true");

            if (!string.IsNullOrEmpty(order))
                query = query.OrderBy(order);

            totalCount = query.Count();

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);
            //else
            //    query = query.Take(15);
            return query;
        }
        public T FirstOrDefault(Expression<Func<T, bool>> filter, string search = "", bool ShowDeleted = false, params Expression<Func<T, object>>[] includePropery)
        {
            int totalCount;
            return Find(filter, search, ShowDeleted, out totalCount).FirstOrDefault();
        }
        public void Create(T entity)
        {
            this._AASTHAContext.Set<T>().Add(entity);
        }
        public void Update(T entity, params Expression<Func<T, object>>[] updatedProperties)
        {
            var dbEntityEntry = _AASTHAContext.Entry(entity);
            if (updatedProperties.Any())
                foreach (var property in updatedProperties)
                    dbEntityEntry.Property(property).IsModified = true;
            else
            {              
                foreach (var property in dbEntityEntry.OriginalValues.Properties)
                {
                    var orgVal = dbEntityEntry.Property(property.Name).OriginalValue;
                    var currVal = dbEntityEntry.Property(property.Name).CurrentValue;                 

                    if (currVal != null && !property.IsPrimaryKey() && orgVal != currVal)
                    {
                        //int o;
                        //bool result = Int32.TryParse(Convert.ToString(currVal), out o);
                        //dbEntityEntry.Property(property.Name).CurrentValue = o == 0 ? orgVal : currVal;
                        dbEntityEntry.Property(property.Name).IsModified = true;
                    }

                }
            }
        }
        public void Delete(T entity, bool? deletePhysical = false)
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
