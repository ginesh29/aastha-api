using System;
using System.Collections.Generic;
using System.Text;

namespace AASTHA2.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IPatientRepository Patients { get; }
        IOpdRepository Opds { get; }
        IIpdRepository Ipds { get; }
        void SaveChanges();
    }
}
