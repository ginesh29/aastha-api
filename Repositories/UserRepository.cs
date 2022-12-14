using AASTHA2.Entities.Models;
using AASTHA2.Repositories.Interfaces;

namespace AASTHA2.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AASTHA2Context AASTHA2Context)
            : base(AASTHA2Context)
        {
        }
    }
}
