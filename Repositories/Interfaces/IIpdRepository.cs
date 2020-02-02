using AASTHA2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AASTHA2.Interfaces
{
    public interface IIpdRepository : IRepository<Ipd>
    {
        IEnumerable<Sp_GetCollection_Result> GetStatistics();
    }
}
