using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Repositories
{
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(AASTHA2Context AASTHA2Context)
            : base(AASTHA2Context)
        {
        }
        public IEnumerable<Sp_GetCollection_Result> GetStatistics(int? Year)
        {
            var result = _AASTHA2Context.Set<Sp_GetCollection_Result>().FromSql("GetPatientStatistics");
            if (Year > 0)
                result = result.Where(m => m.Year == Year);
            return result;
        }
    }
}
