using AASTHA2.Entities;
using AASTHA2.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Repositories
{
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(AASTHAContext AASTHAContext)
            : base(AASTHAContext)
        {
        }
        public IEnumerable<dynamic> GetStatistics(out int totalCount, string filter)
        {
            return Find(null, out totalCount, filter)
                  .GroupBy(grp => new { Month = grp.CreatedDate.Month, Year = grp.CreatedDate.Year })
                  .Select(g => new
                  {
                      Year = g.Key.Year,
                      Month = g.Key.Month,
                      MonthName = g.FirstOrDefault().CreatedDate.ToString("MMMM"),
                      TotalPatient = g.Count(),
                  });
        }
    }
}
