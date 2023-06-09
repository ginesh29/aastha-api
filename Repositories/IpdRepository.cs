﻿using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.EntityFrameworkCore;
using StoredProcedureEFCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Repositories
{
    public class IpdRepository : RepositoryBase<Ipd>, IIpdRepository
    {
        public IpdRepository(AASTHA2Context AASTHA2Context)
            : base(AASTHA2Context)
        {
        }
        public IEnumerable<Sp_GetStatistics_Result> GetStatistics(int? Year)
        {
            IEnumerable<Sp_GetStatistics_Result> rows = null;
            _AASTHA2Context.LoadStoredProc("GetIpdStatistics").Exec(r => rows = r.ToList<Sp_GetStatistics_Result>());
            if (Year > 0)
                rows = rows.Where(m => m.Year == Year);
            return rows;
        }
    }
}
