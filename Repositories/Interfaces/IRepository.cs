using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AASTHA2.Interfaces
{
    public interface IRepository<T>
    {
        // Sync Method
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, out int totalCount, string filter = "", string includeProperties = "", string order = "", int skip = 0, int take = 0);
        T FirstOrDefault(Expression<Func<T, bool>> predicate, string filter = "", string includeProperties = "");
        void Create(T entity);
        //void CreateRange(IEnumerable<T> entities);
        void Update(T entity, params Expression<Func<T, object>>[] updatedProperties);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(T entity, bool deletePhysical = false);
        void DeleteRange(IEnumerable<T> entities, bool deletePhysical = false);
        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);

        //Async Method
        //Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> filter = null);
        //Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null);
    }
}
