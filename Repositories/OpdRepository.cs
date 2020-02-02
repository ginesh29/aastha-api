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
        public OpdRepository(AASTHAContext AASTHAContext)
            : base(AASTHAContext)
        {

        }
        public IEnumerable<Sp_GetCollection_Result> GetStatistics()
        {
            return _AASTHAContext.Set<Sp_GetCollection_Result>().FromSql("GetOpdStatistics");
        }
    }
}
