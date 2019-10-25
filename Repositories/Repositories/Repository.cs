using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public IQueryable<T> Find(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int take = 0, int skip = 0)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }
            if (take > 0)
            {
                query = query.Take(take);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int take = 0, int skip = 0)
        {
            return Find(filter, orderBy, includeProperties, take, skip).FirstOrDefault();
        }
        public bool IsExist(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int take = 0, int skip = 0)
        {
            return Find(filter, orderBy, includeProperties, take, skip).Any();
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

        public async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int take = 0, int skip = 0)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }
            if (take > 0)
            {
                query = query.Take(take);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int take = 0, int skip = 0)
        {
            return await Find(filter, orderBy, includeProperties, take, skip).FirstOrDefaultAsync();
        }
    }
}
