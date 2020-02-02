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
        public PatientRepository(AASTHAContext AASTHAContext)
            : base(AASTHAContext)
        {
        }
        public IEnumerable<Sp_GetCollection_Result> GetStatistics()
        {
            return _AASTHAContext.Set<Sp_GetCollection_Result>().FromSql("GetPatientStatistics");
        }
    }
}
