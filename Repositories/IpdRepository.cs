using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Repositories
{
    public class IpdRepository : RepositoryBase<Ipd>, IIpdRepository
    {
        public IpdRepository(AASTHAContext AASTHAContext)
            : base(AASTHAContext)
        {
        }
        public IEnumerable<Sp_GetCollection_Result> GetStatistics(int? Year)
        {
            var result = _AASTHAContext.Set<Sp_GetCollection_Result>().FromSql("GetIpdStatistics");
            if (Year > 0)
                result = result.Where(m => m.Year == Year);
            return result;
        }
    }
}
