using AASTHA2.Entities;
using AASTHA2.Interfaces;

namespace AASTHA2.Repositories
{
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(AASTHAContext AASTHAContext)
            : base(AASTHAContext)
        {
        }
    }
}
