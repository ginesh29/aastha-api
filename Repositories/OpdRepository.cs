using AASTHA2.Entities;
using AASTHA2.Interfaces;
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
        public IEnumerable<dynamic> GetStatistics(out int totalCount,string filter)
        {
            var data = Find(null, out totalCount,filter)
                    .GroupBy(grp => new { Month = grp.Date.Month, Year = grp.Date.Year })
                    .Select(g => new { Month = g.Key.Month, Year = g.Key.Year, Total = g.Count() })
                    .OrderByDescending(a => a.Year).ThenByDescending(a => a.Month);
            return data;
        }
    }
}
