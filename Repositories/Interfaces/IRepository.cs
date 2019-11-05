using System;
using System.Linq;
using System.Linq.Expressions;

namespace AASTHA2.Interfaces
{
    public interface IRepository<T>
    {
        // Sync Method
        IQueryable<T> Find(Expression<Func<T, bool>> filter = null, string order = null, int skip = 0, int take = 0);
        T FirstOrDefault(Expression<Func<T, bool>> filter = null,  string order = null, int skip = 0, int take = 0);
        bool IsExist(Expression<Func<T, bool>> filter = null,  string order = null, int skip = 0, int take = 0);
        void Create(T entity);
        void Update(T entity, params Expression<Func<T, object>>[] updatedProperties);
        void Delete(T entity);

        //Async Method
        //Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> filter = null);
        //Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null);
    }
}
