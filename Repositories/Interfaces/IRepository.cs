using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AASTHA2.Interfaces
{
    public interface IRepository<T>
    {
        // Sync Method
        IQueryable<T> Find(Expression<Func<T, bool>> filter = null, string search = "", bool? ShowDeleted = false, string order = null, int skip = 0, int take = 15);
        T FirstOrDefault(Expression<Func<T, bool>> filter = null, string search = "", bool? ShowDeleted = false);
        bool IsExist(Expression<Func<T, bool>> filter = null, string search = "", bool? ShowDeleted = false);
        void Create(T entity);
        void Update(T entity, params Expression<Func<T, object>>[] updatedProperties);
        void Delete(T entity, bool? deletePhysical = false);
        int Count(Expression<Func<T, bool>> filter = null, string search = "", bool? ShowDeleted = false);
        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);

        //Async Method
        //Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> filter = null);
        //Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null);
    }
}
