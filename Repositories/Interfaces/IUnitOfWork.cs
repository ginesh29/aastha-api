using System;
using System.Collections.Generic;
using System.Text;

namespace AASTHA2.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        void SaveChanges();
    }
}
