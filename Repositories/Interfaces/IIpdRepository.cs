using AASTHA2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AASTHA2.Interfaces
{
    public interface IIpdRepository : IRepository<Ipd>
    {
        IEnumerable<Sp_GetStatistics_Result> GetStatistics(int? Year);
    }
}
