using AASTHA2.Common;
using AASTHA2.Entities;
using System;
using System.Collections.Generic;

namespace AASTHA2.DTO
{
    public class IpdLookupDTO
    {
        public long id { get; set; }
        public long lookupId { get; set; }
        public LookupDTO lookup { get; set; }
    }    
}
