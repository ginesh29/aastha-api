using AASTHA2.Entities;
using AASTHA2.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AASTHA2.Repositories
{
    public class IpdLookupRepository : RepositoryBase<IpdLookup>, IIpdLookupRepository
    {
        public IpdLookupRepository(AASTHA2Context AASTHA2Context)
            : base(AASTHA2Context)
        {
        }
    }
}
