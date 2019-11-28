using AASTHA2.Entities;
using AASTHA2.Interfaces;

namespace AASTHA2.Repositories
{
    public class LookupRepository : RepositoryBase<Lookup>, ILookupRepository
    {
        public LookupRepository(AASTHAContext AASTHAContext)
            : base(AASTHAContext)
        {
        }
    }
}
