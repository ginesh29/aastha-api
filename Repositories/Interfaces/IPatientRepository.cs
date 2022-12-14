using AASTHA2.Entities.Models;
using System.Collections.Generic;

namespace AASTHA2.Repositories.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        IEnumerable<Sp_GetCollection_Result> GetStatistics(int? Year);
    }
}
