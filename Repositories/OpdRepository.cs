using AASTHA2.Entities;
using AASTHA2.Interfaces;
using System;
using System.Linq;

namespace AASTHA2.Repositories
{
    public class OpdRepository : RepositoryBase<Opd>, IOpdRepository
    {
        public OpdRepository(AASTHAContext AASTHAContext)
            : base(AASTHAContext)
        {

        }
        public IQueryable<dynamic> GetStatistics(out int totalCount, string filter)
        {
            return Find(m => m.Date != Convert.ToDateTime("01/01/1900"), out totalCount, filter)
                  .GroupBy(grp => new { Month = grp.Date.Month, Year = grp.Date.Year })
                  .Select(g => new
                  {
                      Month = g.Key.Month,
                      MonthName = g.FirstOrDefault().Date.ToString("MMMM"),
                      Year = g.Key.Year,
                      TotalPatient = g.Count(),
                      TotalCollection = g.Sum(m => m.ConsultCharge) + g.Sum(m => m.UsgCharge) + g.Sum(m => m.UptCharge) + g.Sum(m => m.InjectionCharge) + g.Sum(m => m.OtherCharge)
                  }).OrderByDescending(m => m.Year);
        }
    }
}
