using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AASTHA2.Repositories
{
    public class OpdRepository : RepositoryBase<Opd>, IOpdRepository
    {
        public OpdRepository(AASTHA2Context AASTHA2Context)
            : base(AASTHA2Context)
        {

        }
        public IEnumerable<Sp_GetCollection_Result> GetStatistics(int? Year)
        {
            var result = _AASTHA2Context.Set<Sp_GetCollection_Result>().FromSql("GetOpdStatistics");
            if (Year > 0)
                result = result.Where(m => m.Year == Year);
            return result;
        }
    }
}
