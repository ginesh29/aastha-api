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

        public IQueryable<T> Find(Expression<Func<T, bool>> filter = null, string search = "", bool? ShowDeleted = false, string order = null, int skip = 0, int take =0)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(search))
            {
                string dynamicQuery;
                object[] param;
                DynamicLinqHelper.DynamicSearchQuery(search, out dynamicQuery, out param);
                query = query.Where(dynamicQuery, param);
            }

            if ((bool)ShowDeleted)
                query = query.Where("IsDeleted!=true");

            if (!string.IsNullOrEmpty(order))
                query = query.OrderBy(order);

            if (skip > 0)
                query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            return query;
        }
        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string search = "", bool? ShowDeleted = false)
        {
            return Find(filter, search, ShowDeleted).FirstOrDefault();
        }
        public bool IsExist(Expression<Func<T, bool>> filter = null, string search = "", bool? ShowDeleted = false)
        {
            return Find(filter, search, ShowDeleted).Any();
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


                    if (currVal != null && !property.IsPrimaryKey() && !orgVal.Equals(currVal))
                    {
                        int o;
                        bool result = Int32.TryParse(Convert.ToString(currVal), out o);
                        dbEntityEntry.Property(property.Name).CurrentValue = o == 0 ? orgVal : currVal;
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
        public int Count(Expression<Func<T, bool>> filter = null, string search = "", bool? ShowDeleted = false)
        {
            return Find(filter, search, ShowDeleted).Count();
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
