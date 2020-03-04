using AASTHA2.Entities;
using AASTHA2.Interfaces;

namespace AASTHA2.Repositories
{
    public class OperationRepository : RepositoryBase<Operation>, IOperationRepository
    {
        public OperationRepository(AASTHA2Context AASTHA2Context)
            : base(AASTHA2Context)
        {
        }
    }
}
