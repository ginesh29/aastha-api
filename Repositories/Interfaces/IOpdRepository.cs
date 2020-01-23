using AASTHA2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AASTHA2.Interfaces
{
    public interface IOpdRepository : IRepository<Opd>
    {
        IEnumerable<dynamic> GetStatistics(out int totalCount, string filter);
    }
}
