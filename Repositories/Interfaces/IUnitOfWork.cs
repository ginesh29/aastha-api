using System;
using System.Collections.Generic;
using System.Text;

namespace AASTHA2.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        void Save();
    }
}
