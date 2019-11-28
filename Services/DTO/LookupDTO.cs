using AASTHA2.Common;
using System;
using System.Collections.Generic;

namespace AASTHA2.DTO
{
    public class LookupDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public LookupType Type { get; set; }
        public long? ParentId { get; set; }

        public LookupDTO Parent { get; set; }
        public ICollection<LookupDTO> Children { get; set; }
    }
}
