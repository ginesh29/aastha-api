using AASTHA2.Entities.Models;
using System.Collections.Generic;

namespace AASTHA2.Repositories.Interfaces
{
    public interface IOpdRepository : IRepository<Opd>
    {
        IEnumerable<Sp_GetCollection_Result> GetStatistics(int? Year);
    }
}
