using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;

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
