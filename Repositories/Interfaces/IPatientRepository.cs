using AASTHA2.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AASTHA2.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        IEnumerable<dynamic> GetStatistics(out int totalCount, string filter);
    }
}
