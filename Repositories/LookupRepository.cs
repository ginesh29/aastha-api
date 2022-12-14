using AASTHA2.Entities.Models;
using AASTHA2.Repositories.Interfaces;

namespace AASTHA2.Repositories
{
    public class LookupRepository : RepositoryBase<Lookup>, ILookupRepository
    {
        public LookupRepository(AASTHA2Context AASTHA2Context)
            : base(AASTHA2Context)
        {
        }
    }
}
