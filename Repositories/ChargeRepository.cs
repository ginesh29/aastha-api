using AASTHA2.Entities;
using AASTHA2.Interfaces;

namespace AASTHA2.Repositories
{
    public class ChargeRepository : RepositoryBase<Charge>, IChargeRepository
    {
        public ChargeRepository(AASTHAContext AASTHAContext)
            : base(AASTHAContext)
        {
        }
    }
}
