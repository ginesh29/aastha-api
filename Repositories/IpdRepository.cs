using AASTHA2.Entities;
using AASTHA2.Interfaces;
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
        public IEnumerable<dynamic> GetStatistics(out int totalCount, string filter)
        {
            return Find(null, out totalCount, filter, "Charges")
                  .GroupBy(grp => new { Month = grp.DischargeDate.Month, Year = grp.DischargeDate.Year })
                  .Select(g => new
                  {
                      Year = g.Key.Year,
                      Month = g.Key.Month,
                      MonthName = g.FirstOrDefault().DischargeDate.ToString("MMMM"),
                      TotalPatient = g.Count(),
                      TotalCollection = g.SelectMany(x => x.Charges).Sum(d => d.Rate * d.Days) - g.Sum(m => m.Discount)
                  }).OrderByDescending(m => m.Year);
        }
    }
}
