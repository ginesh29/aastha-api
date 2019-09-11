using AASTHA2.Entities;
using AASTHA2.Repositories.Interfaces;

namespace AASTHA2.Repositories.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AASTHAContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
