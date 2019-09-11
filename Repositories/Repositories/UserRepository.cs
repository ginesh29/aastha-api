using AASTHA2.Entities;
using AASTHA2.Interfaces;

namespace AASTHA2.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AASTHAContext AASTHAContext)
            : base(AASTHAContext)
        {
        }
    }
}
